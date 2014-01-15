using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApertureCMS.Models
{
    public class Page
    {
        public int PageId { get; set; }
        public string Title { get; set; }
        [Display(Name = "Set As Homepage")]
        public bool IsHomepage { get; set; }
        [UIHint("tinymce_full")]
        public string Content { get; set; }
        public int Status { get; set; }
        public bool Enabled { get; set; }
        public DateTime CreatedDate { get; set; }
        [Display(Name = "Permalink")]
        public string Slug { get; set; }
    }
}
