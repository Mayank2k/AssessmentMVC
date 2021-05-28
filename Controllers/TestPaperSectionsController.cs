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
    public class TestPaperSectionsController : Controller
    {
        private AssesmentContext db = new AssesmentContext();

        // GET: TestPaperSections
        //public ActionResult Index()
        //{
        //    var testPaperSections = db.TestPaperSections.Include(t => t.TestPaper);
        //    return View(testPaperSections.ToList());
        //}

        public ActionResult Index(int TestPaperId)
        {
            IQueryable<TestPaperSection> source = (from x in this.db.TestPaperSections
                                                   where x.TestPaperID == TestPaperId
                                                   select x).Include((TestPaperSection t) => t.TestPaper);
            return base.View(source.ToList<TestPaperSection>());
        }

        // GET: TestPaperSections/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestPaperSection testPaperSection = db.TestPaperSections.Find(id);
            if (testPaperSection == null)
            {
                return HttpNotFound();
            }
            return View(testPaperSection);
        }

        // GET: TestPaperSections/Create
        public ActionResult Create()
        {
            ViewBag.TestPaperID = new SelectList(db.TestPapers, "TestPaperID", "TestPaperCode");
            return View();
        }

        // POST: TestPaperSections/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TestPaperSectionID,TestPaperSectionCode,Description,Instructions,Time,Marks,TestPaperID,Active,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate")] TestPaperSection testPaperSection)
        {
            if (ModelState.IsValid)
            {
                db.TestPaperSections.Add(testPaperSection);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TestPaperID = new SelectList(db.TestPapers, "TestPaperID", "TestPaperCode", testPaperSection.TestPaperID);
            return View(testPaperSection);
        }

        // GET: TestPaperSections/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestPaperSection testPaperSection = db.TestPaperSections.Find(id);
            if (testPaperSection == null)
            {
                return HttpNotFound();
            }
            ViewBag.TestPaperID = new SelectList(db.TestPapers, "TestPaperID", "TestPaperCode", testPaperSection.TestPaperID);
            return View(testPaperSection);
        }

        // POST: TestPaperSections/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TestPaperSectionID,TestPaperSectionCode,Description,Instructions,Time,Marks,TestPaperID,Active,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate")] TestPaperSection testPaperSection)
        {
            if (ModelState.IsValid)
            {
                db.Entry(testPaperSection).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TestPaperID = new SelectList(db.TestPapers, "TestPaperID", "TestPaperCode", testPaperSection.TestPaperID);
            return View(testPaperSection);
        }

        // GET: TestPaperSections/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestPaperSection testPaperSection = db.TestPaperSections.Find(id);
            if (testPaperSection == null)
            {
                return HttpNotFound();
            }
            return View(testPaperSection);
        }

        // POST: TestPaperSections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TestPaperSection testPaperSection = db.TestPaperSections.Find(id);
            db.TestPaperSections.Remove(testPaperSection);
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
