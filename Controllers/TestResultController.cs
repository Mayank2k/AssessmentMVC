using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AssessmentMVC.Models;
using System.Configuration;
using System.Data.SqlClient;
using ClosedXML.Excel;
using System.IO;

namespace AssessmentMVC.Controllers
{
    [Authorize]
    public class TestResultController : Controller
    {
        private AssesmentContext db = new AssesmentContext();

        // GET: TestResult
        //public ActionResult Index()
        //{
        //    var testResults = db.TestResults.Include(t => t.School).Include(t => t.TestCycle).Include(t => t.TestPaper).Include(t => t.User);
        //    return View(testResults.ToList());
        //}

        public ActionResult Index(int? SchoolId = 0)
        {
            IQueryable<TestResult> source;
            ViewBag.SchoolID = new SelectList(this.db.Schools.Where(x => x.Active).OrderBy( x=> x.SchoolName), "SchoolID", "SchoolName", SchoolId);
            
            source = (from x in this.db.TestResults
                        where (int?)x.SchoolID == SchoolId
                        select x).Include((TestResult t) => t.School).Include((TestResult t) => t.TestPaper).Include((TestResult t) => t.User);

            return base.View(source.ToList<TestResult>());
        }

        // GET: TestResult/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestResult testResult = db.TestResults.Find(id);
            if (testResult == null)
            {
                return HttpNotFound();
            }
            return View(testResult);
        }

        // GET: TestResult/Create
        public ActionResult Create()
        {
            ViewBag.SchoolID = new SelectList(db.Schools.Where(x => x.Active).OrderBy( x=> x.SchoolName), "SchoolID", "SchoolCode");
            ViewBag.TestCycleID = new SelectList(db.TestCycles, "TestCycleID", "TestCycle1");
            ViewBag.TestPaperID = new SelectList(db.TestPapers, "TestPaperID", "TestPaperCode");
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FirstName");
            return View();
        }

        // POST: TestResult/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TestResultID,TestCycleID,SchoolID,TestPaperID,UserID,ExamDate,Attempted,QuestionsAttempted,MarksObtained,TimeTaken,Active,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate")] TestResult testResult)
        {
            if (ModelState.IsValid)
            {
                db.TestResults.Add(testResult);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SchoolID = new SelectList(db.Schools.Where(x => x.Active).OrderBy( x=> x.SchoolName), "SchoolID", "SchoolCode", testResult.SchoolID);
            ViewBag.TestCycleID = new SelectList(db.TestCycles, "TestCycleID", "TestCycle1", testResult.TestCycleID);
            ViewBag.TestPaperID = new SelectList(db.TestPapers, "TestPaperID", "TestPaperCode", testResult.TestPaperID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FirstName", testResult.UserID);
            return View(testResult);
        }

        // GET: TestResult/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestResult testResult = db.TestResults.Find(id);
            if (testResult == null)
            {
                return HttpNotFound();
            }
            ViewBag.SchoolID = new SelectList(db.Schools.Where(x => x.Active).OrderBy( x=> x.SchoolName), "SchoolID", "SchoolCode", testResult.SchoolID);
            ViewBag.TestCycleID = new SelectList(db.TestCycles, "TestCycleID", "TestCycle1", testResult.TestCycleID);
            ViewBag.TestPaperID = new SelectList(db.TestPapers, "TestPaperID", "TestPaperCode", testResult.TestPaperID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FirstName", testResult.UserID);
            return View(testResult);
        }

        // POST: TestResult/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TestResultID,TestCycleID,SchoolID,TestPaperID,UserID,ExamDate,Attempted,QuestionsAttempted,MarksObtained,TimeTaken,Active,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate")] TestResult testResult)
        {
            if (ModelState.IsValid)
            {
                db.Entry(testResult).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SchoolID = new SelectList(db.Schools.Where(x => x.Active).OrderBy( x=> x.SchoolName), "SchoolID", "SchoolCode", testResult.SchoolID);
            ViewBag.TestCycleID = new SelectList(db.TestCycles, "TestCycleID", "TestCycle1", testResult.TestCycleID);
            ViewBag.TestPaperID = new SelectList(db.TestPapers, "TestPaperID", "TestPaperCode", testResult.TestPaperID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FirstName", testResult.UserID);
            return View(testResult);
        }

        // GET: TestResult/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestResult testResult = db.TestResults.Find(id);
            if (testResult == null)
            {
                return HttpNotFound();
            }
            return View(testResult);
        }

        // POST: TestResult/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TestResult testResult = db.TestResults.Find(id);
            db.TestResults.Remove(testResult);
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

        public ActionResult ExportToExcel(int? SchoolId)
        {
            if (SchoolId == null)
                return RedirectToAction("Index");

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                string strQuery = "Select * from vwTestResultData  Where SchoolID =" + SchoolId + " Order by SchoolID,TestPaperCode,RegistrationNo,QuestionSequence; ";
                using (SqlCommand sqlCommand = new SqlCommand(strQuery))
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
                                xLWorkbook.Worksheets.Add(dataSet.Tables[0], "TestResult");
                                base.Response.Clear();
                                base.Response.Buffer = true;
                                base.Response.Charset = "";
                                base.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                                base.Response.AddHeader("content-disposition", "attachment;filename=AssessmentTestResult_"+ SchoolId + ".xlsx");
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
            return base.RedirectToAction("Index", new
            {
                SchoolId
            });
        }

        public ActionResult Calculate(int? SchoolId)
        {
            if (SchoolId == null)
                return RedirectToAction("Index");

            string arg_1A_0 = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string commandText = "sp_Calculate";
            using (SqlConnection sqlConnection = new SqlConnection(arg_1A_0))
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.CommandText = commandText;
                    sqlCommand.Parameters.Add("@SchoolID", SqlDbType.Int).Value = SchoolId;
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandTimeout = 0;
                    try
                    {
                        sqlConnection.Open();
                        sqlCommand.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {
                    }
                    finally
                    {
                        sqlConnection.Close();
                        sqlConnection.Dispose();
                    }
                }
            }
            return base.RedirectToAction("Index", new{SchoolId});
        }
    }
}
