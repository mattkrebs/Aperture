using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApertureCMS.Models
{
    public class ApertureDataContext : DbContext
    {
         static ApertureDataContext()
        {
            //Database.SetInitializer<BFY_ContentContext>(null);
        }

         public ApertureDataContext()
             : base("Name=ApertureDataContext")
        {
        }

         public DbSet<BlogEntry> BlogEntries { get; set; }
         public DbSet<Gallery> Galleries { get; set; }
         public DbSet<Photo> Photos { get; set; }
    }
}
