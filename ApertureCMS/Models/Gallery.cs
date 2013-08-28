using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ApertureCMS.Models
{
    public class Gallery
    {

        public Gallery()
        {
            this.Photos = new List<Photo>();
        }

        [Key]
        public int GalleryId { get; set; }
        public string Title { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }
        public string Tags { get; set; }
        public bool Enabled { get; set; }

        [NotMapped]
        public List<int> PhotoIds { get; set; }


    }
}
