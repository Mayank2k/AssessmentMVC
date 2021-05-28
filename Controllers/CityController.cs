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
    [Authorize]
    public class CityController : Controller
    {
        private AssesmentContext db = new AssesmentContext();

        // GET: City
        //public ActionResult Index()
        //{
        //    var cities = db.Cities.Include(c => c.Country).Include(c => c.State);
        //    return View(cities.ToList());
        //}

        public ActionResult Index(string stateCode)
        {
            IQueryable<City> source = (from s in this.db.Cities
                                       where s.StateCode == stateCode
                                       select s).Include((City c) => c.Country).Include((City c) => c.State);
            return base.View(source.ToList<City>());
        }

        // GET: City/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            City city = db.Cities.Find(id);
            if (city == null)
            {
                return HttpNotFound();
            }
            return View(city);
        }

        // GET: City/Create
        public ActionResult Create()
        {
            ViewBag.CountryCode = new SelectList(db.Countries, "CountryCode", "CountryName");
            ViewBag.StateCode = new SelectList(db.States, "StateCode", "StateName");
            return View();
        }

        // POST: City/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CityCode,CityName,StateCode,CountryCode,Active,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate")] City city)
        {
            if (ModelState.IsValid)
            {
                db.Cities.Add(city);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CountryCode = new SelectList(db.Countries, "CountryCode", "CountryName", city.CountryCode);
            ViewBag.StateCode = new SelectList(db.States, "StateCode", "StateName", city.StateCode);
            return View(city);
        }

        // GET: City/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            City city = db.Cities.Find(id);
            if (city == null)
            {
                return HttpNotFound();
            }
            ViewBag.CountryCode = new SelectList(db.Countries, "CountryCode", "CountryName", city.CountryCode);
            ViewBag.StateCode = new SelectList(db.States, "StateCode", "StateName", city.StateCode);
            return View(city);
        }

        // POST: City/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CityCode,CityName,StateCode,CountryCode,Active,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate")] City city)
        {
            if (ModelState.IsValid)
            {
                db.Entry(city).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CountryCode = new SelectList(db.Countries, "CountryCode", "CountryName", city.CountryCode);
            ViewBag.StateCode = new SelectList(db.States, "StateCode", "StateName", city.StateCode);
            return View(city);
        }

        // GET: City/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            City city = db.Cities.Find(id);
            if (city == null)
            {
                return HttpNotFound();
            }
            return View(city);
        }

        // POST: City/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            City city = db.Cities.Find(id);
            db.Cities.Remove(city);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
