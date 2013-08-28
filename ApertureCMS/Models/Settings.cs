using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApertureCMS.Models
{
    public class Settings
    {
        [Key]
        public int SettingsId { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }

    }

    [NotMapped]
    public class SiteSettings
    {
        public List<Settings> Settings { get; set; }
      
        public static string UploadPath
        {
            get
            {
                var value = "";
                using (var db = new ApertureDataContext())
                {
                    value = db.Settings.Single(s => s.Key == "UploadPath").Value;
                }
                return value ?? "/Content/img/";
            }
        }
        public static string ThumbnailWidth
        {
            get
            {
                var value = "";
                using (var db = new ApertureDataContext())
                {
                    value = db.Settings.Single(s => s.Key == "ThumbnailWidth").Value;
                }
                return value ?? "100";
            }
        }
    }


}
