﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ApertureCMS.Models;

namespace ApertureCMS.Admin.Controllers
{
    [Authorize]
    public class BlogController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();


        //
        // GET: /Blog/

        public ActionResult Index()
        {
            return View(unitOfWork.BlogRepository.Get());
        }

        //
        // GET: /Blog/Details/5

        public ActionResult Details(int id = 0)
        {
            BlogEntry blogentry = unitOfWork.BlogRepository.GetByID(id);
            if (blogentry == null)
            {
                return HttpNotFound();
            }
            return View(blogentry);
        }

        //
        // GET: /Blog/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Blog/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)] 
        public ActionResult Create(BlogEntry blogentry)
        {
            if (ModelState.IsValid)
            {
                blogentry.CreatedDate = DateTime.Now;
                unitOfWork.BlogRepository.Insert(blogentry);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }

            return View(blogentry);
        }

        //
        // GET: /Blog/Edit/5

        public ActionResult Edit(int id = 0)
        {
            BlogEntry blogentry = unitOfWork.BlogRepository.GetByID(id);
            if (blogentry == null)
            {
                return HttpNotFound();
            }
            return View(blogentry);
        }

        //
        // POST: /Blog/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)] 
        public ActionResult Edit(BlogEntry blogentry)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.BlogRepository.Update(blogentry);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(blogentry);
        }

        //
        // GET: /Blog/Delete/5

        public ActionResult Delete(int id = 0)
        {
            BlogEntry blogentry = unitOfWork.BlogRepository.GetByID(id);
            if (blogentry == null)
            {
                return HttpNotFound();
            }
            return View(blogentry);
        }

        //
        // POST: /Blog/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            
            unitOfWork.BlogRepository.Delete(id);
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