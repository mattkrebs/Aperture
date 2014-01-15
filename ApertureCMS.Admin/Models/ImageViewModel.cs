using ApertureCMS.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ApertureCMS.Admin.Models
{
    public class ImageViewModel
    {
        public ImageViewModel()
        {
           
        }
        public int PhotoId { get; set; }
        public bool Selected { get; set; }
        public string PhotoUrl { get; set; }
        public string ThumbUrl { get; set; }


        public ImageViewModel(Photo photo, bool selected)
        {
            this.PhotoUrl = photo.PhotoUrl;
            this.ThumbUrl = photo.ThumbnailUrl;
            this.PhotoId = photo.PhotoId;
            this.Selected = selected;



        }
       

      


    }


   
}