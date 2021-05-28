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
    public class TestPaperSectionQuestionsController : Controller
    {
        private AssesmentContext db = new AssesmentContext();

        // GET: TestPaperSectionQuestions
        //public ActionResult Index()
        //{
        //    var testPaperSectionQuestions = db.TestPaperSectionQuestions.Include(t => t.QuestionMaster).Include(t => t.TestPaper).Include(t => t.TestPaperSection);
        //    return View(testPaperSectionQuestions.ToList());
        //}
        public ActionResult Index(int TestPaperId, int TestPaperSectionId)
        {
            IQueryable<TestPaperSectionQuestion> source = (from x in this.db.TestPaperSectionQuestions
                                                           where x.TestPaperID == TestPaperId && x.TestPaperSectionID == TestPaperSectionId
                                                           select x).Include((TestPaperSectionQuestion t) => t.QuestionMaster).Include((TestPaperSectionQuestion t) => t.TestPaper).Include((TestPaperSectionQuestion t) => t.TestPaperSection);
            return base.View(source.OrderBy(x => x.QuestionSequence).ToList<TestPaperSectionQuestion>());
        }

        // GET: TestPaperSectionQuestions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestPaperSectionQuestion testPaperSectionQuestion = db.TestPaperSectionQuestions.Find(id);
            if (testPaperSectionQuestion == null)
            {
                return HttpNotFound();
            }
            return View(testPaperSectionQuestion);
        }

        // GET: TestPaperSectionQuestions/Create
        public ActionResult Create(int TestPaperId)
        {
            ViewBag.TestPaperID = new SelectList(from x in this.db.TestPapers
                                                 where x.TestPaperID == TestPaperId
                                                 select x, "TestPaperID", "TestPaperCode");
            ViewBag.TestPaperSectionID = new SelectList(from x in this.db.TestPaperSections
                                                        where x.TestPaperID == TestPaperId
                                                        select x, "TestPaperSectionID", "TestPaperSectionCode");
            ViewBag.TestPaperCode = (from x in this.db.TestPapers where x.TestPaperID == TestPaperId select x).Select(x => x.TestPaperCode).FirstOrDefault();
            ViewBag.CourseName = (from c in this.db.Courses join TP in this.db.TestPapers on c.CourseID equals TP.CourseID
                                          where TP.TestPaperID == TestPaperId select c).Select(x => x.CourseName).FirstOrDefault();

            //IQueryable<QuestionMaster> Questions = (from qm in this.db.QuestionMasters
            //                                        join qt in this.db.QuestionTags on qm.QuestionID equals qt.QuestionID
            //                                        join TP in this.db.TestPapers on qt.CourseID equals TP.CourseID                                                    
            //                                        where TP.TestPaperID == TestPaperId && qm.Active && new[] { "A", "S" }.Contains(qm.StatusID)
            //                                        select qm);
            //ViewBag.QuestionID = new SelectList(Questions.ToList(), "QuestionID", "QuestionCode");

            ViewBag.QuestionSequence = 1;
            if (db.TestPaperSectionQuestions.Any(x => x.TestPaperID == TestPaperId))
            {
                ViewBag.QuestionSequence = db.TestPaperSectionQuestions.Where(x => x.TestPaperID == TestPaperId).Max(x => x.QuestionSequence) + 1;
            }

            //return View();
            return PartialView("Create");
            //var testPaperSectionQuestion = new TestPaperSectionQuestion();
            //return PartialView("Create", testPaperSectionQuestion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TestPaperSectionQuestionID,TestPaperID,TestPaperSectionID,QuestionID,QuestionSequence,Active,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate")] TestPaperSectionQuestion testPaperSectionQuestion)
        {
            testPaperSectionQuestion.TestPaperSectionID = (from x in this.db.TestPaperSections
                                                           where x.TestPaperID == testPaperSectionQuestion.TestPaperID
                                                           select x).First<TestPaperSection>().TestPaperSectionID;
            if (testPaperSectionQuestion.TestPaperSectionID > 0)
            {
                base.ModelState.Remove("TestPaperSectionID");
            }
            IQueryable<QuestionMaster> Questions = (from qm in this.db.QuestionMasters
                                                    join qt in this.db.QuestionTags on qm.QuestionID equals qt.QuestionID
                                                    //join TP in this.db.TestPapers on qt.CourseID equals TP.CourseID
                                                    where qm.QuestionCode == testPaperSectionQuestion.QuestionID.ToString() && qm.Active && qm.StatusID.Equals("A")
                                                    select qm);
            int questionID = Questions.Select(x => x.QuestionID).FirstOrDefault();
            if (questionID != 0)
            {
                testPaperSectionQuestion.QuestionID = questionID;
            }
            else
            {
                base.ModelState.AddModelError("QuestionID", "Question Code not found ! !");
            }
            if (this.db.TestPaperSectionQuestions.Any((TestPaperSectionQuestion x) => x.TestPaperID == testPaperSectionQuestion.TestPaperID && x.QuestionID == testPaperSectionQuestion.QuestionID))
            {
                base.ModelState.AddModelError("QuestionID", "Question Code already Added ! !");
            }
            if (this.db.TestPaperSectionQuestions.Any((TestPaperSectionQuestion x) => x.TestPaperID == testPaperSectionQuestion.TestPaperID && x.QuestionSequence == testPaperSectionQuestion.QuestionSequence))
            {
                base.ModelState.AddModelError("QuestionSequence", "Question Sequence already Created !");
            }
            if (base.ModelState.IsValid)
            {
                this.db.TestPaperSectionQuestions.Add(testPaperSectionQuestion);
                this.db.SaveChanges();
                //return base.RedirectToAction("Details", "TestPaper", new
                //{
                //    id = testPaperSectionQuestion.TestPaperID
                //});
                return Json(new { success = true });
            }

            //ViewBag.QuestionID = new SelectList(db.QuestionMasters, "QuestionID", "QuestionCode", testPaperSectionQuestion.QuestionID);
            ViewBag.TestPaperID = new SelectList(from x in this.db.TestPapers
                                                 where x.TestPaperID == testPaperSectionQuestion.TestPaperID
                                                 select x, "TestPaperID", "TestPaperCode", testPaperSectionQuestion.TestPaperID);
            ViewBag.TestPaperSectionID = new SelectList(from x in this.db.TestPaperSections
                                                        where x.TestPaperID == testPaperSectionQuestion.TestPaperID
                                                        select x, "TestPaperSectionID", "TestPaperSectionCode", testPaperSectionQuestion.TestPaperSectionID);

            ViewBag.TestPaperCode = (from x in this.db.TestPapers where x.TestPaperID == testPaperSectionQuestion.TestPaperID select x).Select(x => x.TestPaperCode).FirstOrDefault();
            ViewBag.CourseName = (from c in this.db.Courses
                                  join TP in this.db.TestPapers on c.CourseID equals TP.CourseID
                                  where TP.TestPaperID == testPaperSectionQuestion.TestPaperID
                                  select c).Select(x => x.CourseName).FirstOrDefault();
            ViewBag.QuestionSequence = testPaperSectionQuestion.QuestionSequence;
            return PartialView("Create", testPaperSectionQuestion);
        }


        // GET: TestPaperSectionQuestions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestPaperSectionQuestion testPaperSectionQuestion = db.TestPaperSectionQuestions.Find(id);
            if (testPaperSectionQuestion == null)
            {
                return HttpNotFound();
            }
            ViewBag.QuestionID = new SelectList(from x in this.db.QuestionMasters
                                                where x.QuestionID == testPaperSectionQuestion.QuestionID
                                                select x, "QuestionID", "QuestionCode", testPaperSectionQuestion.QuestionID);
            ViewBag.TestPaperID = new SelectList(from x in this.db.TestPapers
                                                 where x.TestPaperID == testPaperSectionQuestion.TestPaperID
                                                 select x, "TestPaperID", "TestPaperCode", testPaperSectionQuestion.TestPaperID);
            ViewBag.TestPaperSectionID = new SelectList(from x in this.db.TestPaperSections
                                                        where x.TestPaperSectionID == testPaperSectionQuestion.TestPaperSectionID
                                                        select x, "TestPaperSectionID", "TestPaperSectionCode", testPaperSectionQuestion.TestPaperSectionID);
            return View(testPaperSectionQuestion);
        }

        // POST: TestPaperSectionQuestions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TestPaperSectionQuestionID,TestPaperID,TestPaperSectionID,QuestionID,QuestionSequence,Active,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate")] TestPaperSectionQuestion testPaperSectionQuestion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(testPaperSectionQuestion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.QuestionID = new SelectList(from x in this.db.QuestionMasters
                                                where x.QuestionID == testPaperSectionQuestion.QuestionID
                                                select x, "QuestionID", "QuestionCode", testPaperSectionQuestion.QuestionID);
            ViewBag.TestPaperID = new SelectList(from x in this.db.TestPapers
                                                 where x.TestPaperID == testPaperSectionQuestion.TestPaperID
                                                 select x, "TestPaperID", "TestPaperCode", testPaperSectionQuestion.TestPaperID);
            ViewBag.TestPaperSectionID = new SelectList(from x in this.db.TestPaperSections
                                                        where x.TestPaperSectionID == testPaperSectionQuestion.TestPaperSectionID
                                                        select x, "TestPaperSectionID", "TestPaperSectionCode", testPaperSectionQuestion.TestPaperSectionID);
            return View(testPaperSectionQuestion);
        }

        // GET: TestPaperSectionQuestions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestPaperSectionQuestion testPaperSectionQuestion = db.TestPaperSectionQuestions.Find(id);
            if (testPaperSectionQuestion == null)
            {
                return HttpNotFound();
            }
            return PartialView("Delete", testPaperSectionQuestion);
        }

        // POST: TestPaperSectionQuestions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TestPaperSectionQuestion testPaperSectionQuestion = db.TestPaperSectionQuestions.Find(id);
            db.TestPaperSectionQuestions.Remove(testPaperSectionQuestion);
            db.SaveChanges();
            //return base.RedirectToAction("Details", "TestPaper", new
            //{
            //    id = testPaperSectionQuestion.TestPaperID
            //});
            return Json(new { success = true });
        }

        //public JsonResult InValidQuestionID(string QuestionID)
        //{
        //    return base.Json(!this.db.QuestionMasters.Any((QuestionMaster x) => x.QuestionCode == QuestionID), JsonRequestBehavior.AllowGet);
        //}

        public JsonResult IsQuestionIDAvailable(int TestPaperID, string QuestionID)
        {
            IQueryable<QuestionMaster> Questions = (from qm in this.db.QuestionMasters
                                                    join qt in this.db.QuestionTags on qm.QuestionID equals qt.QuestionID
                                                    join TP in this.db.TestPapers on qt.CourseID equals TP.CourseID
                                                    where TP.TestPaperID == TestPaperID && qm.QuestionCode == QuestionID && qm.Active && qm.StatusID.Equals("A")
                                                    select qm);
            int questionID = Questions.Select(x => x.QuestionID).FirstOrDefault();
            if (questionID == 0)
            {
                return Json(!(questionID == 0), JsonRequestBehavior.AllowGet);
            }
            return base.Json(!this.db.TestPaperSectionQuestions.Any((TestPaperSectionQuestion x) => x.TestPaperID == TestPaperID && x.QuestionID == questionID), JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsQuestionSequenceAvailable(int TestPaperID, int QuestionSequence)
        {
            return base.Json(!this.db.TestPaperSectionQuestions.Any((TestPaperSectionQuestion x) => x.TestPaperID == TestPaperID && x.QuestionSequence == QuestionSequence), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetQuestionText(int testPaperID, string questionCode)
        {
            IQueryable<QuestionMaster> Questions = (from qm in this.db.QuestionMasters
                                                    join qt in this.db.QuestionTags on qm.QuestionID equals qt.QuestionID
                                                    join TP in this.db.TestPapers on qt.CourseID equals TP.CourseID
                                                    where TP.TestPaperID == testPaperID && qm.QuestionCode == questionCode && qm.Active && qm.StatusID.Equals("A")
                                                    select qm);

            string questionBody = Questions.Select(x => x.QuestionBody).FirstOrDefault();
            if (string.IsNullOrEmpty(questionBody))
            {
                IQueryable<Course> Courses = (from c in this.db.Courses
                                              join TP in this.db.TestPapers on c.CourseID equals TP.CourseID
                                              where TP.TestPaperID == testPaperID
                                              select c);

                string courseName = Courses.Select(x => x.CourseName).FirstOrDefault();
                return base.Json(string.Format("Question Code not found for the course '{0}' !", courseName.ToString()), JsonRequestBehavior.AllowGet);
            }

            return base.Json(questionBody, JsonRequestBehavior.AllowGet);
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
