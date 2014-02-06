using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApertureCMS.Admin.Models
{
    public class GalleryViewModel
    {
        public int GalleryId { get; set; }
        public string Title { get; set; }
        public bool Enabled { get; set; }
        public string Tags { get; set; }
        public int CategoryId { get; set; }
        public string ShareCode { get; set; }
        public bool Protected { get; set; }
        
        public List<ImageViewModel> Photos { get; set; }
    }
}