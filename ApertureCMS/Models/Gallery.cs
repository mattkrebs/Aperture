using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApertureCMS.Models
{
    public class Gallery
    {
        public int GalleryId { get; set; }
        public string Title { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }
        public string Tags { get; set; }
        public bool Enabled { get; set; }

    }
}
