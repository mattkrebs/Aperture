using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ApertureCMS.Admin.Models
{
   
        public class UploadPhotoModel
        {
           

            [Display(Name = "Local file")]
            public HttpPostedFileBase File { get; set; }

            public bool IsFile { get; set; }

            [Range(0, int.MaxValue)]
            public int X { get; set; }

            [Range(0, int.MaxValue)]
            public int Y { get; set; }

            [Range(1, int.MaxValue)]
            public int Width { get; set; }

            [Range(1, int.MaxValue)]
            public int Height { get; set; }
        }
    
}