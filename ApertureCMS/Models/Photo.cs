using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApertureCMS.Models
{
    public class Photo
    {
        public int PhotoId { get; set; }
        public string PhotoUrl { get; set; }
        public string ThumbnailUrl { get; set; }
        public string Tags { get; set; }
        public bool Enabled { get; set; }
        public virtual ICollection<Gallery> Galleries {get;set;}

    }
}
