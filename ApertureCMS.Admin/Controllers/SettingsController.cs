using ApertureCMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ApertureCMS.Admin.Controllers
{
    [Authorize]
    public class SettingsController : Controller
    {
        //
        // GET: /Settings/
        public ApertureDataContext db = new ApertureDataContext();
        public ActionResult Index()
        {
            var settings = db.Settings.ToList();
            if (settings.Count == 0)
            {
               settings = InitSettings();
            }
            return View(settings);
        }


        private List<Settings> InitSettings()
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
            settings.ForEach(s => db.Settings.Add(s));

            var categories = new List<Category>
            {
                new Category { Name = "Newborn"},
                new Category { Name = "Toddler"},
                new Category { Name = "Kids"},
                new Category { Name = "Family"},
                new Category { Name = "Graduation"},
                new Category { Name = "Wedding"},

            };
            categories.ForEach(s => db.Categories.Add(s));


            db.SaveChanges();

            return settings;
        }

        [HttpPost]
        public ActionResult Index(List<Settings> setting)
        {
            foreach (var item in setting)
            {
                var s = db.Settings.Find(item.SettingsId);

                s.Value = item.Value;
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

    }
}
