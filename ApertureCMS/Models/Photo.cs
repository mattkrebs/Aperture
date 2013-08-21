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
      

    }
}
