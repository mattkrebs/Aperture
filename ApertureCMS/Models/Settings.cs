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
        public string Title { get; set; }
        public string Type { get; set; }

    }

    [NotMapped]
    public class SiteSettings
    {
        public List<Settings> Settings { get; set; }
      
        public static string UploadPath
        {
            get
            {
                return GetValue("UploadPath");
            }
        }
        public static int MaxImageWidth
        {
            get
            {
                return int.Parse(GetValue("MaxImageWidth"));
            }
        }
        public static int MaxImageHeight
        {
            get
            {
                return int.Parse(GetValue("MaxImageHeight"));
            }
        }
        public static string AzureAccountName {
            get
            {
                return GetValue("AzureAccountName");

            }
        }

        public static string AzureStorageKey
        {
            get
            {
                return GetValue("AzureStorageKey");
            }
        }


        public static string GetValue(string key)
        {
            var value = "";
            using (var db = new ApertureDataContext())
            {
                value = db.Settings.Single(s => s.Key == key).Value;
            }
            return value ?? "";
        }
    }


}
