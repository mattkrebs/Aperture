using ApertureCMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ApertureCMS.Admin.Controllers
{
    [Authorize]
    public class PageController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        //
        // GET: /Page/

        public ActionResult Index()
        {
            return View(unitOfWork.PageRepository.Get());
        }

        //
        // GET: /Page/Details/5

        public ActionResult Details(int id = 0)
        {
            Page page = unitOfWork.PageRepository.GetByID(id);
            if (page == null)
            {
                return HttpNotFound();
            }
            return View(page);
        }

        //
        // GET: /Page/Create

        public ActionResult Create()
        {
            //var newPage = new Page()
            //{
            //    ContentItems = new List<ContentItem>()
            //    {
            //        new ContentItem()
            //    }
            //};
            //return View(newPage);
            return View();
        }

        //
        // POST: /Page/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)] 
        public ActionResult Create(Page page)
        {
            try
            {
                // TODO: Add insert logic here
                page.CreatedDate = DateTime.Now;
                unitOfWork.PageRepository.Insert(page);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Page/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Page page = unitOfWork.PageRepository.GetByID(id);
            if (page == null)
            {
                return HttpNotFound();
            }
            return View(page);
        }

        //
        // POST: /Page/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)] 
        public ActionResult Edit(Page page)
        {
            try
            {
                // TODO: Add update logic here
                unitOfWork.PageRepository.Update(page);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Page/Delete/5

        public ActionResult Delete(int id = 0)
        {
            try
            {
                // TODO: Add delete logic here
                unitOfWork.PageRepository.Delete(id);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // POST: /Page/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                // TODO: Add delete logic here
                unitOfWork.PageRepository.Delete(id);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}
