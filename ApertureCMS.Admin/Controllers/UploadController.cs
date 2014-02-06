using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ApertureCMS.Admin.Models;
using ApertureCMS.Models;
using ImageResizer;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure;
using System.Configuration;
using Microsoft.WindowsAzure.Storage.Blob;

namespace ApertureCMS.Admin.Controllers
{
    [Authorize]
    public class UploadController : Controller
    {

        ApertureDataContext db = new ApertureDataContext();
        //
        // GET: /Upload/
        public ActionResult Index()
        {
            var images = db.Photos.ToList();
            return View(images);
        }


        [HttpPost]
        public ActionResult SaveFiles(string qqfile)
        {
            HttpPostedFileBase file = Request.Files[0];
            Dictionary<string, string> versions = new Dictionary<string, string>();
            try
            {
                //Define the versions to generate
                versions.Add("_thumb", "width=" + 100 + "&height=" + 100 + "&crop=auto&format=jpg"); //Crop to square thumbnail
                versions.Add("_medium", "maxwidth=400&maxheight=400&format=jpg"); //Fit inside 400x400 area, jpeg
                versions.Add("_large", "maxwidth=1900&maxheight=1900&format=jpg"); //Fit inside 1900x1200 area
                using (var db = new ApertureDataContext())
                {
                    if (file.ContentLength > 0)
                    { //Skip unused file controls.

                        string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                        string extension = Path.GetExtension(file.FileName);


                        //Generate each version
                        foreach (string suffix in versions.Keys)
                        {

                            UploadFilesToStorageAccount(file, fileName + suffix, versions[suffix]);
                        }

                        var photo = new Photo()
                        {
                            PhotoUrl = SiteSettings.UploadPath + fileName + "_large.jpg",
                            ThumbnailUrl = SiteSettings.UploadPath + fileName + "_thumb.jpg",
                            MediumPhotoUrl = SiteSettings.UploadPath + fileName + "_medium.jpg",
                        };


                        db.Photos.Add(photo);
                        db.SaveChanges();
                    }


                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, "application/json");
            }
            return Json(new { success = true }, "text/html");
        }

        public void UploadFilesToStorageAccount(HttpPostedFileBase file, string newFileName, string resizeSettings)
        {
            //  CloudStorageAccount storageAccount =    CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            CloudStorageAccount storageAccount = new CloudStorageAccount(new Microsoft.WindowsAzure.Storage.Auth.StorageCredentials(SiteSettings.AzureAccountName, SiteSettings.AzureStorageKey), false);
            var storageClient = storageAccount.CreateCloudBlobClient();
            var storageContainer = storageClient.GetContainerReference(ConfigurationManager.AppSettings.Get("CloudStorageContainerReference"));

            storageContainer.CreateIfNotExists();
            // Retrieve reference to the blob we want to create            
            CloudBlockBlob blockBlob = storageContainer.GetBlockBlobReference(newFileName + ".jpg");

            // Populate our blob with contents from the uploaded file.
            using (var ms = new MemoryStream())
            {
                ImageResizer.ImageJob ij = new ImageResizer.ImageJob(file.InputStream, ms, new ImageResizer.ResizeSettings(resizeSettings), false, false);
                ij.ResetSourceStream = true;
                ij.Build();

                blockBlob.Properties.ContentType = "image/jpeg";
                ms.Seek(0, SeekOrigin.Begin);
                blockBlob.UploadFromStream(ms);
            }
        }

       


    }
}