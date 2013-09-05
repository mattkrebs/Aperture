using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace ApertureCMS.Models
{
    public class ApertureDataContext : DbContext
    {
         static ApertureDataContext()
        {
            Database.SetInitializer<ApertureDataContext>(new ApertureDbInitializer());

        }

         public ApertureDataContext()
             : base("Name=ApertureDataContext")
        {
        }
         public DbSet<Settings> Settings { get; set; }
         public DbSet<BlogEntry> BlogEntries { get; set; }
         public DbSet<Gallery> Galleries { get; set; }
         public DbSet<Photo> Photos { get; set; }
         public DbSet<User> Users { get; set; }
         public DbSet<Role> Roles { get; set; }

    }

    public class ApertureDbInitializer : DropCreateDatabaseIfModelChanges<ApertureDataContext>
    {
        protected override void Seed(ApertureDataContext context)
        {

            WebSecurity.Register("Demo", "123456", "demo@demo.com", true, "Demo", "Demo");
            Roles.CreateRole("Admin");
            Roles.AddUserToRole("Demo", "Admin");

            var settings = new List<Settings>
            {
                new Settings { Key = "UploadPath", Value = "/Content/img/" },
                new Settings { Key = "ThumbnailWidth", Value = "100" },
                new Settings { Key = "PhotoWidth", Value = "0" },
            };
            settings.ForEach(s => context.Settings.Add(s));
            context.SaveChanges();
        }
    }
   
}
