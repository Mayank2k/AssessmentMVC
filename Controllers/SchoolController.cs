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
    public class SchoolController : Controller
    {
        private AssesmentContext db = new AssesmentContext();

        // GET: School
        //public ActionResult Index()
        //{
        //    var schools = db.Schools.Include(s => s.Board).Include(s => s.City).Include(s => s.Country).Include(s => s.School2).Include(s => s.State);
        //    return View(schools.ToList());
        //}

        public ActionResult Index(string searchBy, string search)
        {
            if (searchBy == "Code")
            {
                IQueryable<School> source = (from x in this.db.Schools
                                             where x.SchoolCode == search || search == null
                                             select x).Include((School s) => s.Board).Include((School s) => s.City).Include((School s) => s.Country).Include((School s) => s.School2).Include((School s) => s.State);
                return base.View(source.ToList<School>().OrderBy(x => x.SchoolName));
            }
            if (searchBy == "Name")
            {
                IQueryable<School> source2 = (from x in this.db.Schools
                                              where x.SchoolName.StartsWith(search) || search == null
                                              select x).Include((School s) => s.Board).Include((School s) => s.City).Include((School s) => s.Country).Include((School s) => s.School2).Include((School s) => s.State);
                return base.View(source2.ToList<School>().OrderBy(x => x.SchoolName));
            }
            IQueryable<School> source3 = this.db.Schools.Include((School s) => s.Board).Include((School s) => s.Country).Include((School s) => s.State).Include((School s) => s.City).Include((School s) => s.School2);
            return base.View(source3.ToList<School>().Where(x => x.Active).OrderBy(x => x.SchoolName));
        }

        // GET: School/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            School school = db.Schools.Find(id);
            if (school == null)
            {
                return HttpNotFound();
            }
            return View(school);
        }

        // GET: School/Create
        public ActionResult Create()
        {
            ViewBag.BoardID = new SelectList(db.Boards, "BoardID", "BoardName");
            ViewBag.CityCode = new SelectList(db.Cities, "CityCode", "CityName");
            ViewBag.CountryCode = new SelectList(db.Countries, "CountryCode", "CountryName");
            ViewBag.ParentSchoolID = new SelectList(db.Schools, "SchoolID", "SchoolCode");
            ViewBag.StateCode = new SelectList(db.States, "StateCode", "StateName");
            return View();
        }

        // POST: School/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SchoolID,SchoolCode,SchoolName,BoardID,AcademicSessionStart,AcademicSessionEnd,SPOCName,SPOCPhone,SPOCEmail,SPOCFax,AddressLine1,AddressLine2,CityCode,ZipCode,StateCode,CountryCode,SchoolLogo,ParentSchoolID,Active,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate")] School school)
        {
            if (this.db.Schools.Any((School x) => x.SchoolCode == school.SchoolCode))
            {
                base.ModelState.AddModelError("SchoolCode", "School Code already Exist !");
            }
            if (ModelState.IsValid)
            {
                db.Schools.Add(school);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BoardID = new SelectList(db.Boards, "BoardID", "BoardName", school.BoardID);
            ViewBag.CityCode = new SelectList(db.Cities, "CityCode", "CityName", school.CityCode);
            ViewBag.CountryCode = new SelectList(db.Countries, "CountryCode", "CountryName", school.CountryCode);
            ViewBag.ParentSchoolID = new SelectList(db.Schools, "SchoolID", "SchoolCode", school.ParentSchoolID);
            ViewBag.StateCode = new SelectList(db.States, "StateCode", "StateName", school.StateCode);
            return View(school);
        }

        // GET: School/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            School school = db.Schools.Find(id);
            if (school == null)
            {
                return HttpNotFound();
            }
            ViewBag.BoardID = new SelectList(db.Boards, "BoardID", "BoardName", school.BoardID);
            ViewBag.CityCode = new SelectList(db.Cities, "CityCode", "CityName", school.CityCode);
            ViewBag.CountryCode = new SelectList(db.Countries, "CountryCode", "CountryName", school.CountryCode);
            ViewBag.ParentSchoolID = new SelectList(from x in this.db.Schools
                                                    where (int?)x.SchoolID != id
                                                    select x, "SchoolID", "SchoolCode", school.ParentSchoolID);
            ViewBag.StateCode = new SelectList(db.States, "StateCode", "StateName", school.StateCode);
            return View(school);
        }

        // POST: School/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SchoolID,SchoolCode,SchoolName,BoardID,AcademicSessionStart,AcademicSessionEnd,SPOCName,SPOCPhone,SPOCEmail,SPOCFax,AddressLine1,AddressLine2,CityCode,ZipCode,StateCode,CountryCode,SchoolLogo,ParentSchoolID,Active,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate")] School school)
        {
            if (ModelState.IsValid)
            {
                db.Entry(school).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BoardID = new SelectList(db.Boards, "BoardID", "BoardName", school.BoardID);
            ViewBag.CityCode = new SelectList(db.Cities, "CityCode", "CityName", school.CityCode);
            ViewBag.CountryCode = new SelectList(db.Countries, "CountryCode", "CountryName", school.CountryCode);
            ViewBag.ParentSchoolID = new SelectList(db.Schools, "SchoolID", "SchoolCode", school.ParentSchoolID);
            ViewBag.StateCode = new SelectList(db.States, "StateCode", "StateName", school.StateCode);
            return View(school);
        }

        // GET: School/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            School school = db.Schools.Find(id);
            if (school == null)
            {
                return HttpNotFound();
            }
            return View(school);
        }

        // POST: School/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            School school = db.Schools.Find(id);
            db.Schools.Remove(school);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public JsonResult IsSchoolCodeAvailable(string schoolCode)
        {
            return base.Json(!this.db.Schools.Any((School x) => x.SchoolCode == schoolCode), JsonRequestBehavior.AllowGet);
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
