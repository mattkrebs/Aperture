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
        public ActionResult UploadPhotos(IEnumerable<HttpPostedFileBase> files)
        {
            var size = SiteSettings.ThumbnailWidth;
            Dictionary<string, string> versions = new Dictionary<string, string>();
            //Define the versions to generate
            versions.Add("_thumb", "width=" + size + "&height=" + size + "&crop=auto&format=jpg"); //Crop to square thumbnail
            versions.Add("_medium", "maxwidth=400&maxheight=400&format=jpg"); //Fit inside 400x400 area, jpeg
            versions.Add("_large", "maxwidth=1900&maxheight=1900&format=jpg"); //Fit inside 1900x1200 area
            using (var db = new ApertureDataContext())
            {
                foreach (var file in files)
                {

                    if (file.ContentLength <= 0) continue; //Skip unused file controls.

                    string guid = Guid.NewGuid().ToString();
                    //Get the physical path for the uploads folder and make sure it exists
                    

                    //Generate each version
                    foreach (string suffix in versions.Keys)
                    {
                        UploadFilesToStorageAccount(file, guid + suffix);
                        //Generate a filename (GUIDs are best).
                      //  string fullFileName = Path.Combine(uploadFolder, guid + suffix);

                        //Let the image builder add the correct extension based on the output file type
                       // fullFileName = ImageBuilder.Current.Build(file, fullFileName, new ResizeSettings(versions[suffix]), false, true);


                    }

                    var photo = new Photo()
                    {
                        PhotoUrl = SiteSettings.UploadPath + guid + "_large.jpg",
                        ThumbnailUrl = SiteSettings.UploadPath + guid + "_thumb.jpg",
                        MediumPhotoUrl = SiteSettings.UploadPath + guid + "_medium.jpg",
                    };

                    db.Photos.Add(photo);
                }

                db.SaveChanges();
            }

            return Json("All files have been successfully stored.");
        }

        public void UploadFilesToStorageAccount(HttpPostedFileBase file, string newFileName)
        {
            CloudStorageAccount storageAccount =    CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            var storageClient = storageAccount.CreateCloudBlobClient();
            var storageContainer = storageClient.GetContainerReference(ConfigurationManager.AppSettings.Get("CloudStorageContainerReference"));

            storageContainer.CreateIfNotExists();

            
                string fileName = Path.GetFileName(file.FileName);
                if (file != null && file.ContentLength > 0)
                {
                    CloudBlockBlob azureBlockBlob = storageContainer.GetBlockBlobReference(newFileName);
                    azureBlockBlob.UploadFromStream(file.InputStream);
                }
            
        }
        private byte[] ReadData(Stream stream)
        {
            byte[] buffer = new byte[16 * 1024];

            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }

                return ms.ToArray();
            }
        }

    }
}
