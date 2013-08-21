using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApertureCMS.Models
{
    public class BlogEntry
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public virtual Gallery Gallery { get; set; }
        public string Tags { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Enabled { get; set; }
     }


}
