using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ApertureCMS.Models
{
    public class Category : ILookup
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
