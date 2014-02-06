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
         public DbSet<Category> Categories { get; set; }
         public DbSet<Page> Pages { get; set; }
         public DbSet<ContentItem> ContentItems { get; set; }
         public DbSet<ContentType> ContentTypes { get; set; }


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
                new Settings { Key = "UploadPath", Value = "/Content/img/", Title= "Upload Path" , Type = "text"},
                new Settings { Key = "MaxImageWidth", Value = "1200" , Title= "Max Image Width" , Type = "text"},
                new Settings { Key = "MaxImageHeight", Value = "1200" , Title= "Max Image Height" , Type = "text"},
                new Settings { Key = "UseAzureStorage", Value = "1", Title= "Use Azure Storage" , Type = "text" },                
                new Settings { Key = "AzureAccountName", Value = "kimjansen" , Title= "Azure Storage Account Name" , Type = "text"},
                new Settings { Key = "AzureStorageKey", Value = "hgCJEbpepdnV52CUMeJAWh2U2ViOp5Dkv0WOmbcjz2MO4Xixz7iqMS42QGieltop2NTXnoButt3mgEaX3IYcHw==" , Title= "Azure Storage Key" , Type = "text"},
            };
            settings.ForEach(s => context.Settings.Add(s));

            var categories = new List<Category>
            {
                new Category { Name = "Newborn"},
                new Category { Name = "Toddler"},
                new Category { Name = "Kids"},
                new Category { Name = "Family"},
                new Category { Name = "Graduation"},
                new Category { Name = "Wedding"},

            };
            categories.ForEach(s => context.Categories.Add(s));


            context.SaveChanges();
        }
    }
   
}
