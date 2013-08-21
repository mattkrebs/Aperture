using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Web;

namespace ApertureCMS.Models
{
    public class Photo
    {
        [Key]
        public int PhotoId { get; set; }
        public string PhotoUrl { get; set; }
        public string ThumbnailUrl { get; set; }
        public string Tags { get; set; }
        public bool Enabled { get; set; }
        public virtual ICollection<Gallery> Galleries {get;set;}

        [Display(Name = "Local file")]
        [NotMapped]
        public HttpPostedFileBase File { get; set; }

        [NotMapped]
        public bool IsFile { get; set; }

        [Range(0, int.MaxValue)]
        [NotMapped]
        public int X { get; set; }

        [Range(0, int.MaxValue)]
        [NotMapped]
        public int Y { get; set; }

        [Range(1, int.MaxValue)]
        [NotMapped]
        public int Width { get; set; }

        [Range(1, int.MaxValue)]
        [NotMapped]
        public int Height { get; set; }

    }
}
