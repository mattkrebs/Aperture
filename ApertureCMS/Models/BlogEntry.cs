using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApertureCMS.Models
{
    public class BlogEntry
    {
        [Key]
        public int EntryId { get; set; }
        public string Title { get; set; }
        [UIHint("tinymce_full")]
        public string Content { get; set; }
        public virtual Gallery Gallery { get; set; }
        public string Tags { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Enabled { get; set; }
        public int Status { get; set; }
        public Nullable<DateTime> PublishDate { get; set; }
        public Category Category { get; set; }
        public string Description { get; set; }

        [ForeignKey("Category")]
        public int Category_Id { get; set; }
        
   

     }


}
