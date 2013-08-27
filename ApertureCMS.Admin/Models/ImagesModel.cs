using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ApertureCMS.Admin.Models
{
    public class ImagesModel
    {
        public ImagesModel()
        {
            Images = new List<string>();
        }

        public List<string> Images { get; set; }



        public static ImagesModel GetAllImages(string virtualPath)
        {
            var path = HttpContext.Current.Request.MapPath(virtualPath);


            var images = new ImagesModel();
            //Read out files from the files directory
            var files = Directory.GetFiles(path);
            //Add them to the model
            foreach (var file in files)
                images.Images.Add(virtualPath + Path.GetFileName(file));

            return images;
        }


      


    }


   
}