using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ApertureCMS.Models;
using ApertureCMS.Admin.Models;

namespace ApertureCMS.Admin.Controllers
{
    public class GalleryController : Controller
    {
        private ApertureDataContext db = new ApertureDataContext();

        //
        // GET: /Gallery/

        public ActionResult Index()
        {
            return View(db.Galleries.ToList());
        }

        //
        // GET: /Gallery/Details/5

        public ActionResult Details(int id = 0)
        {
            Gallery gallery = db.Galleries.Find(id);
            if (gallery == null)
            {
                return HttpNotFound();
            }
            return View(gallery);
        }

        //
        // GET: /Gallery/Create

        public ActionResult Create()
        {
            Gallery gallery = new Gallery();

            return View(gallery);
        }

        //
        // POST: /Gallery/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Gallery gallery)
        {
            if (ModelState.IsValid)
            {
                db.Galleries.Add(gallery);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(gallery);
        }

        //
        // GET: /Gallery/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Gallery gallery = db.Galleries.Find(id);
            if (gallery == null)
            {
                return HttpNotFound();
            }
            var images = db.Photos.ToList();
            ViewBag.Images = images;
          

            return View(gallery);
        }

        //
        // POST: /Gallery/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Gallery gallery)
        {
            //get current entry from db (db is context)
            var item = db.Entry<Gallery>(gallery);

            //change item state to modified
            item.State = EntityState.Modified;

            //load existing items for ManyToMany collection
            item.Collection(i => i.Photos).Load();

            //clear Photo items          
            gallery.Photos.Clear();

            //add Toner items
            foreach (var id in gallery.PhotoIds)
            {
                var photo = db.Photos.Find(id);
                gallery.Photos.Add(photo);
            }

            if (ModelState.IsValid)
            {
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gallery);
        }

        //
        // GET: /Gallery/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Gallery gallery = db.Galleries.Find(id);
            if (gallery == null)
            {
                return HttpNotFound();
            }
            return View(gallery);
        }

        //
        // POST: /Gallery/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Gallery gallery = db.Galleries.Find(id);
            db.Galleries.Remove(gallery);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}