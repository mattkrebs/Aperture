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
        private UnitOfWork unitOfWork = new UnitOfWork();

        //
        // GET: /Gallery/

        public ActionResult Index()
        {
            return View(unitOfWork.GalleryRepostitory.Get());
        }

        //
        // GET: /Gallery/Details/5

        public ActionResult Details(int id = 0)
        {
            Gallery gallery = unitOfWork.GalleryRepostitory.GetByID(id);
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

            return View(LoadImages(gallery));
        }

        //
        // POST: /Gallery/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GalleryViewModel gallery)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Gallery gal = new Gallery();

                    gal.Enabled = gallery.Enabled;
                    gal.Tags = gallery.Tags;
                    gal.Title = gallery.Title;
                    gal.Category_Id = gallery.CategoryId;
                    gal.Photos.Clear();

                    foreach (var item in gallery.Photos.Where(x => x.Selected))
                    {
                        gal.Photos.Add(unitOfWork.PhotoRepostitory.GetByID(item.PhotoId));
                    }

                    unitOfWork.GalleryRepostitory.Insert(gal);
                    unitOfWork.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }

            return View(gallery);
        }

        //
        // GET: /Gallery/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Gallery gallery = unitOfWork.GalleryRepostitory.Get(filter: d => d.GalleryId == id, includeProperties: "Photos").FirstOrDefault();
            if (gallery == null)
            {
                return HttpNotFound();
            }

            return View(LoadImages(gallery));
        }

        private GalleryViewModel LoadImages(Gallery gallery)
        {
            GalleryViewModel galleryVM = new GalleryViewModel()
            {
                GalleryId = gallery.GalleryId,
                Enabled = gallery.Enabled,
                Tags = gallery.Tags,
                Title = gallery.Title,
                CategoryId = gallery.Category_Id
            };

            var ids = gallery.Photos.Select(x => x.PhotoId);
            var images = unitOfWork.PhotoRepostitory.Get().Select(x => new ImageViewModel(x, ids.Contains(x.PhotoId))).ToList();
            galleryVM.Photos = images;

            return galleryVM;
        }

        //
        // POST: /Gallery/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(GalleryViewModel gallery)
        {
            Gallery gal = unitOfWork.GalleryRepostitory.GetByID(gallery.GalleryId);
            try
            {
                if (ModelState.IsValid)
                {
                    gal.Enabled = gallery.Enabled;
                    gal.Tags = gallery.Tags;
                    gal.Title = gallery.Title;
                    gal.Photos.Clear();

                    foreach (var item in gallery.Photos.Where(x => x.Selected))
                    {
                        gal.Photos.Add(unitOfWork.PhotoRepostitory.GetByID(item.PhotoId));
                    }
                    unitOfWork.GalleryRepostitory.Update(gal);
                    unitOfWork.Save();

                    
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }


            return View(LoadImages(gal));
        }

        public ActionResult DeletePhoto(int id, int galleryid)
        {
           
            try
            {
                unitOfWork.PhotoRepostitory.Delete(id);
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", "Unable to delete. Try again, and if the problem persists, see your system administrator.");
            }
            return RedirectToAction("Edit", new { id = galleryid });
        }


        //
        // GET: /Gallery/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Gallery gallery = unitOfWork.GalleryRepostitory.GetByID(id);
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
            Gallery course = unitOfWork.GalleryRepostitory.GetByID(id);
            unitOfWork.GalleryRepostitory.Delete(id);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}