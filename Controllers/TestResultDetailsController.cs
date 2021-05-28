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
    public class TestResultDetailsController : Controller
    {
        private AssesmentContext db = new AssesmentContext();

        // GET: TestResultDetails
        //public ActionResult Index()
        //{
        //    var testResultDetails = db.TestResultDetails.Include(t => t.QuestionMaster).Include(t => t.School).Include(t => t.TestCycle).Include(t => t.TestPaper).Include(t => t.User);
        //    return View(testResultDetails.ToList());
        //}

        public ActionResult Index(int TestCycleId, int SchoolId, int TestPaperId, int UserId)
        {
            IQueryable<TestResultDetail> source = (from x in this.db.TestResultDetails
                                                   where x.TestCycleID == TestCycleId && x.TestPaperID == TestPaperId && x.UserID == UserId
                                                   select x).Include((TestResultDetail t) => t.QuestionMaster).Include((TestResultDetail t) => t.TestCycle).Include((TestResultDetail t) => t.TestPaper).Include((TestResultDetail t) => t.User);
            return base.View(source.ToList<TestResultDetail>());
        }

        // GET: TestResultDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestResultDetail testResultDetail = db.TestResultDetails.Find(id);
            if (testResultDetail == null)
            {
                return HttpNotFound();
            }
            return View(testResultDetail);
        }

        // GET: TestResultDetails/Create
        public ActionResult Create()
        {
            ViewBag.QuestionID = new SelectList(db.QuestionMasters, "QuestionID", "QuestionCode");
            ViewBag.SchoolID = new SelectList(db.Schools.Where(x => x.Active).OrderBy( x=> x.SchoolName), "SchoolID", "SchoolCode");
            ViewBag.TestCycleID = new SelectList(db.TestCycles, "TestCycleID", "TestCycle1");
            ViewBag.TestPaperID = new SelectList(db.TestPapers, "TestPaperID", "TestPaperCode");
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FirstName");
            return View();
        }

        // POST: TestResultDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TestResultDetailID,TestCycleID,SchoolID,TestPaperID,UserID,QuestionID,QuestionSequence,CorrectOption,Attempted,UserOption,MarksObtained,ScoringWeight,TimeTaken,Active,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate")] TestResultDetail testResultDetail)
        {
            if (ModelState.IsValid)
            {
                db.TestResultDetails.Add(testResultDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.QuestionID = new SelectList(db.QuestionMasters, "QuestionID", "QuestionCode", testResultDetail.QuestionID);
            ViewBag.SchoolID = new SelectList(db.Schools.Where(x => x.Active).OrderBy( x=> x.SchoolName), "SchoolID", "SchoolCode", testResultDetail.SchoolID);
            ViewBag.TestCycleID = new SelectList(db.TestCycles, "TestCycleID", "TestCycle1", testResultDetail.TestCycleID);
            ViewBag.TestPaperID = new SelectList(db.TestPapers, "TestPaperID", "TestPaperCode", testResultDetail.TestPaperID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FirstName", testResultDetail.UserID);
            return View(testResultDetail);
        }

        // GET: TestResultDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestResultDetail testResultDetail = db.TestResultDetails.Find(id);
            if (testResultDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.QuestionID = new SelectList(db.QuestionMasters, "QuestionID", "QuestionCode", testResultDetail.QuestionID);
            ViewBag.SchoolID = new SelectList(db.Schools.Where(x => x.Active).OrderBy( x=> x.SchoolName), "SchoolID", "SchoolCode", testResultDetail.SchoolID);
            ViewBag.TestCycleID = new SelectList(db.TestCycles, "TestCycleID", "TestCycle1", testResultDetail.TestCycleID);
            ViewBag.TestPaperID = new SelectList(db.TestPapers, "TestPaperID", "TestPaperCode", testResultDetail.TestPaperID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FirstName", testResultDetail.UserID);
            return View(testResultDetail);
        }

        // POST: TestResultDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TestResultDetailID,TestCycleID,SchoolID,TestPaperID,UserID,QuestionID,QuestionSequence,CorrectOption,Attempted,UserOption,MarksObtained,ScoringWeight,TimeTaken,Active,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate")] TestResultDetail testResultDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(testResultDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.QuestionID = new SelectList(db.QuestionMasters, "QuestionID", "QuestionCode", testResultDetail.QuestionID);
            ViewBag.SchoolID = new SelectList(db.Schools.Where(x => x.Active).OrderBy( x=> x.SchoolName), "SchoolID", "SchoolCode", testResultDetail.SchoolID);
            ViewBag.TestCycleID = new SelectList(db.TestCycles, "TestCycleID", "TestCycle1", testResultDetail.TestCycleID);
            ViewBag.TestPaperID = new SelectList(db.TestPapers, "TestPaperID", "TestPaperCode", testResultDetail.TestPaperID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FirstName", testResultDetail.UserID);
            return View(testResultDetail);
        }

        // GET: TestResultDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestResultDetail testResultDetail = db.TestResultDetails.Find(id);
            if (testResultDetail == null)
            {
                return HttpNotFound();
            }
            return View(testResultDetail);
        }

        // POST: TestResultDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TestResultDetail testResultDetail = db.TestResultDetails.Find(id);
            db.TestResultDetails.Remove(testResultDetail);
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
