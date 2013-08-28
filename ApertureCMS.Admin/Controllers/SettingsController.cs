﻿using ApertureCMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ApertureCMS.Admin.Controllers
{
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
            var settings = new List<Settings>
            {
                new Settings { Key = "UploadPath", Value = "/Content/img/" },
                new Settings { Key = "ThumbnailWidth", Value = "100" },
                new Settings { Key = "PhotoWidth", Value = "0" },
            };
            settings.ForEach(s => db.Settings.Add(s));
            db.SaveChanges();

            return settings;
        }

    }
}
