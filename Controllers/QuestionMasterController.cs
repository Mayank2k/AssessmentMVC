using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AssessmentMVC.Models;
using PagedList;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;

namespace AssessmentMVC.Controllers
{
    [Authorize]
    public class QuestionMasterController : Controller
    {
        private AssesmentContext db = new AssesmentContext();

        public virtual ActionResult Index(string code, int? courseID, int? unitID, int? topicID, string statusID, int? difficultyLevelID, int? page)
        {
            ViewBag.CourseID = new SelectList(db.Courses.Where(x => x.Active), "CourseID", "CourseName");
            ViewBag.StatusID = new SelectList(db.Status.Where(x => x.Active), "StatusID", "Description");
            ViewBag.DifficultyLevelID = new SelectList(db.DifficultyLevels.Where(x => x.Active), "DifficultyLevelID", "Description");
            IQueryable<QuestionMaster> questionMaster = db.QuestionMasters.Where(x => x.Active).Include(q => q.QuestionTags);
                                                 
            if (!string.IsNullOrEmpty(code))
            {
                questionMaster = questionMaster.Where(x=> x.QuestionCode.Contains(code) || x.QuestionBody.Contains(code)) ;                
            }

            if (!string.IsNullOrEmpty(statusID))
            {
                ViewBag.StatusID = new SelectList(db.Status, "StatusID", "Description", statusID);
                questionMaster = questionMaster.Where(x => x.StatusID.Equals(statusID));
            }

            if (difficultyLevelID != null)
            {
                ViewBag.DifficultyLevelID = new SelectList(db.DifficultyLevels.Where(x => x.Active), "DifficultyLevelID", "Description", difficultyLevelID);
                questionMaster = questionMaster.Where(q => q.QuestionTags.Any(qt => qt.DifficultyLevelID == difficultyLevelID));
            }

            if (courseID != null)
            {
                ViewBag.CourseID = new SelectList(db.Courses.Where(x => x.Active), "CourseID", "CourseName", courseID);
                questionMaster = questionMaster.Where(q=>q.QuestionTags.Any(qt => qt.CourseID == courseID));
            }

            if (unitID != null)
            {
                questionMaster = questionMaster.Where(q => q.QuestionTags.Any(qt => qt.UnitID == unitID));
            }

            if (topicID != null)
            {
                questionMaster = questionMaster.Where(q => q.QuestionTags.Any(qt => qt.TopicID == topicID));
            }
            
            return base.View(questionMaster.OrderByDescending(x => x.QuestionID).ToList<QuestionMaster>().ToPagedList(page ?? 1, 50));
        }


        //public virtual ActionResult Index(string code, int? courseID, int? unitID, int? topicID, string statusID, int? page)
        //{
        //    ViewBag.StatusID = new SelectList(db.Status.Where(x => x.Active), "StatusID", "Description");
        //    ViewBag.CourseID = new SelectList(db.Courses.Where(x=>x.Active), "CourseID", "CourseName");            
        //    if (!string.IsNullOrEmpty(code))
        //    {
        //        IQueryable<QuestionMaster> source = (from x in this.db.QuestionMasters
        //                                             where (x.QuestionCode.Contains(code) || x.QuestionBody.Contains(code)) && x.Active
        //                                             select x).Include(q => q.QuestionType).Include(q => q.Status);
        //        return base.View(source.OrderByDescending(x=>x.QuestionID).ToList<QuestionMaster>().ToPagedList(page ?? 1, 50));                
        //    }

        //    if (!string.IsNullOrEmpty(statusID))
        //    {
        //        ViewBag.StatusID = new SelectList(db.Status, "StatusID", "Description", statusID);
        //        IQueryable<QuestionMaster> source2 = (from qm in this.db.QuestionMasters                                                      
        //                                              where qm.StatusID.Equals(statusID) && qm.Active
        //                                              select qm).Include(q => q.QuestionType).Include(q => q.Status);
        //        return base.View(source2.OrderByDescending(x => x.QuestionID).ToList<QuestionMaster>().ToPagedList(page ?? 1, 50));
        //    }
        //    if (courseID != null)
        //    {
        //        ViewBag.CourseID = new SelectList(db.Courses.Where(x => x.Active), "CourseID", "CourseName", courseID);
        //        IQueryable<QuestionMaster> source2 = (from qm in this.db.QuestionMasters
        //                                              join qt in this.db.QuestionTags on qm.QuestionID equals qt.QuestionID
        //                                              //join cs in this.db.Courses on qt.CourseID equals cs.CourseID
        //                                              where qt.CourseID == courseID && qm.Active
        //                                              select qm).Include(q => q.QuestionType).Include(q => q.Status);
        //        return base.View(source2.OrderByDescending(x => x.QuestionID).ToList<QuestionMaster>().ToPagedList(page ?? 1, 50));
        //    }

        //    if (unitID != null)
        //    {
        //        IQueryable<QuestionMaster> source4 = (from qm in this.db.QuestionMasters
        //                                              join qt in this.db.QuestionTags on qm.QuestionID equals qt.QuestionID
        //                                              where qt.UnitID == unitID && qm.Active
        //                                              select qm).Include(q => q.QuestionType).Include(q => q.Status);
        //        return base.View(source4.OrderByDescending(x => x.QuestionID).ToList<QuestionMaster>().ToPagedList(page ?? 1, 50));
        //    }

        //    if (topicID != null)
        //    {   
        //        IQueryable<QuestionMaster> source5 = (from qm in this.db.QuestionMasters
        //                                              join qt in this.db.QuestionTags on qm.QuestionID equals qt.QuestionID                                                      
        //                                              where qt.TopicID == topicID && qm.Active
        //                                              select qm).Include(q => q.QuestionType).Include(q => q.Status);
        //        return base.View(source5.OrderByDescending(x => x.QuestionID).ToList<QuestionMaster>().ToPagedList(page ?? 1, 50));
        //    }

        //    IQueryable<QuestionMaster> source3 = this.db.QuestionMasters.Where(x=>x.Active).Include(q => q.QuestionType).Include(q => q.Status);
        //    return base.View(source3.OrderByDescending(x => x.QuestionID).ToList<QuestionMaster>().ToPagedList(page ?? 1, 50));            
        //}

        // GET: QuestionMaster/Details/5

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionMaster questionMaster = db.QuestionMasters.Find(id);
            if (questionMaster == null)
            {
                return HttpNotFound();
            }

            ViewBag.SourceQuestionCode = db.QuestionMasters.Where(q => q.QuestionID == questionMaster.ParentQuestionID).Select(x => x.QuestionCode).FirstOrDefault();
            if (User.IsInRole("Operator"))
            {
                ViewBag.StatusID = new SelectList(db.Status.Where(x => new[] { "P", "S" }.Contains(x.StatusID)), "StatusID", "Description", questionMaster.StatusID);
            }
            else if (User.IsInRole("Admin"))
            {
                ViewBag.StatusID = new SelectList(db.Status.Where(x => new[] { "A", "R" }.Contains(x.StatusID)), "StatusID", "Description", questionMaster.StatusID);
            }
            return View(questionMaster);
        }

        // GET: QuestionMaster/Details/5
        public ActionResult UpdateStatus(int id, string StatusID, string userComments)
        {
            QuestionMaster questionMaster = db.QuestionMasters.Find(id);
            if (questionMaster == null)
            {
                return HttpNotFound();
            }
            if (questionMaster.QuestionOptions.Count() == 0 || questionMaster.QuestionTags.Count() == 0)
            {
                string systemComments = "System: ";
                if (questionMaster.QuestionOptions.Count() == 0)
                {
                    systemComments = systemComments + "Question Options are not defined !";
                }
                if (questionMaster.QuestionTags.Count() == 0)
                {
                    systemComments = systemComments + " Question Tags are not defined !";
                }
                questionMaster.Comments = systemComments;
            }

            if (questionMaster.QuestionOptions.Count() > 0 && questionMaster.QuestionTags.Count() > 0 && questionMaster.StatusID != StatusID)
            {
                if (questionMaster.StatusID.Equals("P") && StatusID.Equals("S"))
                {
                    questionMaster.CreatedDate = DateTime.Now;
                }
                questionMaster.StatusID = StatusID;
            }

            if (!string.IsNullOrEmpty(userComments))
            {
                questionMaster.Comments = string.Format("{0}: {1}", User.Identity.Name, userComments);
            }

            db.Entry(questionMaster).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Details", "QuestionMaster", new { id = questionMaster.QuestionID });
        }

        // GET: QuestionMaster/Create
        public ActionResult Create()
        {
            //ViewBag.QuestionID = db.QuestionMasters.Max(x=>x.QuestionID) + 1;
            ViewBag.QuestionTypeID = new SelectList(db.QuestionTypes, "QuestionTypeID", "Description");
            ViewBag.StatusID = new SelectList(db.Status, "StatusID", "Description", "P");
            return View();
        }

        // POST: QuestionMaster/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "QuestionID,QuestionCode,QuestionTypeID,QuestionBody,Marks,StatusID,Active,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate")] QuestionMaster questionMaster)
        {
            if (this.db.TestPapers.Any((TestPaper x) => x.TestPaperCode == questionMaster.QuestionCode))
            {
                base.ModelState.AddModelError("QuestionCode", "Question Code already Exist !");
            }
            questionMaster.StatusID = "P";
            if (ModelState.IsValid)
            {
                db.QuestionMasters.Add(questionMaster);
                db.SaveChanges();
                //return RedirectToAction("Index");
                return base.RedirectToAction("Details", "QuestionMaster", new
                {
                    id = questionMaster.QuestionID
                });
            }

            ViewBag.QuestionTypeID = new SelectList(db.QuestionTypes, "QuestionTypeID", "Description", questionMaster.QuestionTypeID);
            ViewBag.StatusID = new SelectList(db.Status, "StatusID", "Description", questionMaster.StatusID);

            return View(questionMaster);
        }

        // GET: QuestionMaster/Edit/5
        public ActionResult Edit(int? id)
        {
            //UserManager.IsInRole(user.Id, "Admin")
            //ApplicationUserManager UserManager = HttpContext.GetOwinContext().Authentication.User.IsInRole("Admin");


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionMaster questionMaster = db.QuestionMasters.Find(id);
            if (questionMaster == null)
            {
                return HttpNotFound();
            }

            ViewBag.StatusID = new SelectList(db.Status, "StatusID", "Description", questionMaster.StatusID);
            ViewBag.QuestionTypeID = new SelectList(db.QuestionTypes, "QuestionTypeID", "Description", questionMaster.QuestionTypeID);
            ViewBag.SourceQuestionCode = db.QuestionMasters.Where(q => q.QuestionID == questionMaster.ParentQuestionID).Select(x => x.QuestionCode).FirstOrDefault();
            return View(questionMaster);
        }

        // POST: QuestionMaster/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "QuestionID,QuestionCode,QuestionTypeID,QuestionBody,Marks,StatusID,Comments,ParentQuestionID,Active,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate")] QuestionMaster questionMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(questionMaster).State = EntityState.Modified;
                db.SaveChanges();
                return base.RedirectToAction("Details", "QuestionMaster", new
                {
                    id = questionMaster.QuestionID
                });
            }
            ViewBag.StatusID = new SelectList(db.Status, "StatusID", "Description", questionMaster.StatusID);
            ViewBag.QuestionTypeID = new SelectList(db.QuestionTypes, "QuestionTypeID", "Description", questionMaster.QuestionTypeID);
            ViewBag.SourceQuestionCode = db.QuestionMasters.Where(q => q.QuestionID == questionMaster.ParentQuestionID).Select(x => x.QuestionCode).FirstOrDefault();
            //ViewBag.QuestionType = new SelectList(new List<SelectListItem>
            //                                {
            //                                    new SelectListItem {Text = "Multiple Choice Question", Value = "MCQ"},
            //                                    new SelectListItem {Text = "True/False", Value = "T/F"}
            //                                }, "Value", "Text", questionMaster.QuestionType);
            return View(questionMaster);
        }

        // GET: QuestionMaster/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionMaster questionMaster = db.QuestionMasters.Find(id);
            if (questionMaster == null)
            {
                return HttpNotFound();
            }
            return View(questionMaster);
        }

        // POST: QuestionMaster/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            QuestionMaster questionMaster = db.QuestionMasters.Find(id);
            db.QuestionMasters.Remove(questionMaster);
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
        public JsonResult IsQuestionCodeAvailable(string questionCode, int? QuestionID)
        {
            if (QuestionID == null)
            {
                return base.Json(!this.db.QuestionMasters.Any((QuestionMaster x) => x.QuestionCode == questionCode), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return base.Json(!this.db.QuestionMasters.Any((QuestionMaster x) => x.QuestionCode == questionCode && x.QuestionID != QuestionID), JsonRequestBehavior.AllowGet);
            }
        }

        private string ConvertToHTML(string text)
        {
            StringBuilder expr_05 = new StringBuilder();
            expr_05.Append(text);
            expr_05.Replace("&lt;b&gt;", "<b>");
            expr_05.Replace("&lt;/b&gt;", "</b>");
            expr_05.Replace("&lt;u&gt;", "<u>");
            expr_05.Replace("&lt;/u&gt;", "</u>");
            expr_05.Replace("&lt;i&gt;", "<i>");
            expr_05.Replace("&lt;/i&gt;", "</i>");
            return expr_05.ToString();
        }


        public ActionResult CopyQuestion(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionMaster questionMaster = db.QuestionMasters.Find(id);
            if (questionMaster == null)
            {
                return HttpNotFound();
            }

            string arg_1A_0 = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string commandText = "sp_CopyQuestion";
            string newQuestionID = "";
            using (SqlConnection sqlConnection = new SqlConnection(arg_1A_0))
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.CommandText = commandText;
                    sqlCommand.Parameters.Add("@QuestionID", SqlDbType.Int).Value = id;
                    sqlCommand.Parameters.Add("@NewQuestionID", SqlDbType.Int).Direction = ParameterDirection.Output;
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandTimeout = 0;
                    try
                    {
                        sqlConnection.Open();
                        sqlCommand.ExecuteNonQuery();
                        newQuestionID = sqlCommand.Parameters["@NewQuestionID"].Value.ToString();

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
            return base.RedirectToAction("Edit", new { id = newQuestionID });
        }
    }
}
