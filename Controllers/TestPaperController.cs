using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AssessmentMVC.Models;
using ClosedXML.Excel;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;

namespace AssessmentMVC.Controllers
{
    [Authorize]
    public class TestPaperController : Controller
    {
        private AssesmentContext db = new AssesmentContext();

        public ActionResult Index(string code, int? courseID, string testPaperSetCode, int? page)
        {
            ViewBag.CourseID = new SelectList(db.Courses.Where(x => x.Active), "CourseID", "CourseName");
            ViewBag.TestPaperSetCode = new SelectList(db.TestPaperSets.Where(x => x.Active), "TestPaperSetCode", "Description");

            IQueryable<TestPaper> testPapers = db.TestPapers.Include(t => t.Course).Include(t => t.TestPaperSet);
            if (!string.IsNullOrEmpty(code))
            {
                testPapers = testPapers.Where(x => x.TestPaperCode.Contains(code) || x.Description.Contains(code));
            }
            if (courseID != null)
            {
                ViewBag.CourseID = new SelectList(db.Courses.Where(x => x.Active), "CourseID", "CourseName", courseID);
                testPapers = testPapers.Where(x => x.CourseID == courseID);
            }
            if (!string.IsNullOrEmpty(testPaperSetCode))
            {
                ViewBag.TestPaperSetCode = new SelectList(db.TestPaperSets.Where(x => x.Active), "TestPaperSetCode", "Description", testPaperSetCode);
                testPapers = testPapers.Where(x => x.TestPaperSetCode == testPaperSetCode);
            }
            return View(testPapers.OrderByDescending(x => x.TestPaperID).ToList());
        }

        // GET: TestPaper/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestPaper testPaper = db.TestPapers.Find(id);
            if (testPaper == null)
            {
                return HttpNotFound();
            }
            return View(testPaper);
        }

        // GET: TestPaper/Create
        public ActionResult Create()
        {
            ViewBag.CourseID = new SelectList(db.Courses.Where(x => x.Active), "CourseID", "CourseName");
            ViewBag.TestPaperSetCode = new SelectList(db.TestPaperSets, "TestPaperSetCode", "Description");
            return View();
        }

        // POST: TestPaper/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TestPaperID,TestPaperCode,ISBN,Description,Instructions,TestPaperSetCode,ActivationDate,ExpiryDate,Time,Marks,MaxQuestions,CourseID,Active,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate")] TestPaper testPaper)
        {
            if (this.db.TestPapers.Any((TestPaper x) => x.TestPaperCode == testPaper.TestPaperCode))
            {
                base.ModelState.AddModelError("TestPaperCode", "Paper Code already Exist !");
            }
            if (ModelState.IsValid)
            {
                db.TestPapers.Add(testPaper);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseID = new SelectList(db.Courses.Where(x => x.Active), "CourseID", "CourseName", testPaper.CourseID);
            ViewBag.TestPaperSetCode = new SelectList(db.TestPaperSets, "TestPaperSetCode", "Description", testPaper.TestPaperSetCode);
            return View(testPaper);
        }

        // GET: TestPaper/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestPaper testPaper = db.TestPapers.Find(id);
            if (testPaper == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseID = new SelectList(db.Courses.Where(x => x.Active), "CourseID", "CourseName", testPaper.CourseID);
            ViewBag.TestPaperSetCode = new SelectList(db.TestPaperSets, "TestPaperSetCode", "Description", testPaper.TestPaperSetCode);
            return View(testPaper);
        }

        // POST: TestPaper/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TestPaperID,TestPaperCode,ISBN,Description,Instructions,TestPaperSetCode,ActivationDate,ExpiryDate,Time,Marks,MaxQuestions,CourseID,Active,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate")] TestPaper testPaper)
        {
            if (ModelState.IsValid)
            {
                db.Entry(testPaper).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseID = new SelectList(db.Courses.Where(x => x.Active), "CourseID", "CourseName", testPaper.CourseID);
            ViewBag.TestPaperSetCode = new SelectList(db.TestPaperSets, "TestPaperSetCode", "Description", testPaper.TestPaperSetCode);
            return View(testPaper);
        }

        // GET: TestPaper/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestPaper testPaper = db.TestPapers.Find(id);
            if (testPaper == null)
            {
                return HttpNotFound();
            }
            return View(testPaper);
        }

        // POST: TestPaper/Delete/5
        [ActionName("Delete"), HttpPost, ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TestPaper testPaper = this.db.TestPapers.Find(new object[]
            {
                id
            });
            foreach (TestPaperSection current in testPaper.TestPaperSections.ToList<TestPaperSection>())
            {
                this.db.TestPaperSections.Remove(current);
            }
            this.db.TestPapers.Remove(testPaper);
            this.db.SaveChanges();
            return base.RedirectToAction("Index");
        }

        public JsonResult IsPaperCodeAvailable(string testPaperCode)
        {
            return base.Json(!this.db.TestPapers.Any((TestPaper x) => x.TestPaperCode == testPaperCode), JsonRequestBehavior.AllowGet);
        }


        public ActionResult UpdateStatus(int id)
        {
            TestPaper testPaper = db.TestPapers.Where(x => x.TestPaperID == id).FirstOrDefault();
            if (testPaper == null)
            {
                return HttpNotFound();
            }
            if (!testPaper.Active)
            {
                testPaper.Active = true;
                db.Entry(testPaper).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Details", "TestPaper", new { id = testPaper.TestPaperID });
        }

        public void ExportToExcel(string testPaperCode = "", string testPaperSetCode = "")
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                string strQuery1 = "select * From vwTestPaper order by 1; ";
                string strQuery2 = "select * From vwTestPaperQuestions order by 1,2,3; ";
                string strQuery3 = "select * From vwTestPaperQuestionsOptions order by 1,2,3; ";

                string fileName = "Assessment All Test Papers.xlsx";

                if (!string.IsNullOrEmpty(testPaperSetCode))
                {
                    strQuery1 = string.Format("select * From vwTestPaper Where PaperSet = '{0}' order by 1; ", testPaperSetCode);
                    strQuery2 = string.Format("select * From vwTestPaperQuestions Where TestPaperCode in (Select TestPaperCode from TestPaper where TestPaperSetCode = '{0}') order by 1,2,3; ", testPaperSetCode);
                    strQuery3 = string.Format("select * From vwTestPaperQuestionsOptions Where TestPaperCode in (Select TestPaperCode from TestPaper where TestPaperSetCode = '{0}') order by 1,2,3; ", testPaperSetCode);

                    fileName = string.Format("Assessment Test Paper Set {0}.xlsx", testPaperSetCode);
                }

                if (!string.IsNullOrEmpty(testPaperCode))
                {
                    strQuery1 = string.Format("select * From vwTestPaper Where PaperCode = '{0}' order by 1; ", testPaperCode);
                    strQuery2 = string.Format("select * From vwTestPaperQuestions Where TestPaperCode = '{0}' order by 1,2,3; ", testPaperCode);
                    strQuery3 = string.Format("select * From vwTestPaperQuestionsOptions Where TestPaperCode = '{0}' order by 1,2,3; ", testPaperCode);

                    fileName = string.Format("Assessment Test Paper {0}.xlsx", testPaperCode);
                }

                using (SqlCommand sqlCommand = new SqlCommand(strQuery1 + strQuery2 + strQuery3))
                {
                    using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter())
                    {
                        sqlCommand.Connection = sqlConnection;
                        sqlCommand.CommandTimeout = 0;
                        sqlDataAdapter.SelectCommand = sqlCommand;
                        using (DataSet dataSet = new DataSet())
                        {
                            sqlDataAdapter.Fill(dataSet);
                            using (XLWorkbook xLWorkbook = new XLWorkbook())
                            {
                                xLWorkbook.Worksheets.Add(dataSet.Tables[0], "TestPaper");
                                xLWorkbook.Worksheets.Add(dataSet.Tables[1], "TestPaperQuestions");
                                xLWorkbook.Worksheets.Add(dataSet.Tables[2], "TestPaperQuestionsOptions");
                                base.Response.Clear();
                                base.Response.Buffer = true;
                                base.Response.Charset = "";
                                base.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                                base.Response.AddHeader("content-disposition", string.Format("attachment;filename={0}", fileName));
                                
                                using (MemoryStream memoryStream = new MemoryStream())
                                {
                                    xLWorkbook.SaveAs(memoryStream);
                                    memoryStream.WriteTo(base.Response.OutputStream);
                                    base.Response.Flush();
                                    base.Response.End();
                                }
                            }
                        }
                    }
                }
            }
            //return base.RedirectToAction("Index");
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
