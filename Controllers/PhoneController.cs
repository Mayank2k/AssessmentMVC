using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AssessmentMVC.Models;

namespace AssessmentMVC.Controllers
{
    public class PhoneController : Controller
    {
        private AssesmentContext db = new AssesmentContext();


        // GET: /Phone/
        public ActionResult Index(string filter = null, int page = 1, int pageSize = 5, string sort = "PhoneId", string sortdir = "DESC")
        {
            var records = new PagedList<Phone>();
            ViewBag.filter = filter;
            records.Content = db.Phones
                        .Where(x => filter == null ||
                                (x.Model.Contains(filter))
                                   || x.Company.Contains(filter)
                              )
                        //.OrderBy(sort + " " + sortdir)
                        .OrderByDescending(x => x.PhoneId)
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();

            // Count
            records.TotalRecords = db.Phones
                         .Where(x => filter == null ||
                               (x.Model.Contains(filter)) || x.Company.Contains(filter)).Count();

            records.CurrentPage = page;
            records.PageSize = pageSize;

            return View(records);

        }


        // GET: /Phone/Details/5
        public ActionResult Details(int id = 0)
        {
            Phone phone = db.Phones.Find(id);
            if (phone == null)
            {
                return HttpNotFound();
            }
            return PartialView("Details", phone);
        }


        // GET: /Phone/Create
        [HttpGet]
        public ActionResult Create()
        {
            var phone = new Phone();
            return PartialView("Create", phone);
        }


        // POST: /Phone/Create
        [HttpPost]
        public JsonResult Create(Phone phone)
        {
            if (ModelState.IsValid)
            {
                db.Phones.Add(phone);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(phone, JsonRequestBehavior.AllowGet);
        }


        // GET: /Phone/Edit/5
        [HttpGet]
        public ActionResult Edit(int id = 0)
        {
            var phone = db.Phones.Find(id);
            if (phone == null)
            {
                return HttpNotFound();
            }

            return PartialView("Edit", phone);
        }


        // POST: /Phone/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Phone phone)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phone).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true });
            }
            return PartialView("Edit", phone);
        }

        //
        // GET: /Phone/Delete/5
        public ActionResult Delete(int id = 0)
        {
            Phone phone = db.Phones.Find(id);
            if (phone == null)
            {
                return HttpNotFound();
            }
            return PartialView("Delete", phone);
        }


        //
        // POST: /Phone/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var phone = db.Phones.Find(id);
            db.Phones.Remove(phone);
            db.SaveChanges();
            return Json(new { success = true });
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}