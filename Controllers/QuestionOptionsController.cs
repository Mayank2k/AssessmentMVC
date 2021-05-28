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
    public class QuestionOptionsController : Controller
    {
        private AssesmentContext db = new AssesmentContext();

        // GET: QuestionOptions
        //public ActionResult Index()
        //{
        //    var questionOptions = db.QuestionOptions.Include(q => q.Misconception).Include(q => q.QuestionMaster);
        //    return View(questionOptions.ToList());
        //}


        public ActionResult Index(int questionId, string questionCode)
        {
            IQueryable<QuestionOption> source = (from x in this.db.QuestionOptions
                                                 where x.QuestionID == questionId
                                                 select x).Include(q => q.Misconception).Include(q => q.Misconception1).Include(q => q.Misconception2).Include(q => q.QuestionMaster);
            if (!string.IsNullOrEmpty(questionCode))
            {
                ViewBag.QuestionCode = questionCode;
            }
            else
            {
                ViewBag.QuestionCode = (from x in this.db.QuestionMasters
                                        where x.QuestionID == questionId
                                        select x).Single<QuestionMaster>().QuestionCode.ToString();
            }
            return base.View(source.ToList<QuestionOption>());
        }

        // GET: QuestionOptions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionOption questionOption = db.QuestionOptions.Find(id);
            if (questionOption == null)
            {
                return HttpNotFound();
            }
            return View(questionOption);
        }

        // GET: QuestionOptions/Create
        public ActionResult Create(int questionId, string questionCode)
        {
            ViewBag.MisconceptionID = new SelectList(db.Misconceptions, "MisconceptionID", "Description");
            ViewBag.OtherMisconceptionID1 = new SelectList(db.Misconceptions, "MisconceptionID", "Description");
            ViewBag.OtherMisconceptionID2 = new SelectList(db.Misconceptions, "MisconceptionID", "Description");
            ViewBag.QuestionID = new SelectList(db.QuestionMasters, "QuestionID", "QuestionCode", questionId);
            if (!string.IsNullOrEmpty(questionCode))
            {
                ViewBag.QuestionCode = questionCode;
            }
            else
            {
                ViewBag.QuestionCode = (from x in this.db.QuestionMasters
                                        where x.QuestionID == questionId
                                        select x).Single<QuestionMaster>().QuestionCode.ToString();

            }
            ViewBag.OptionID = new SelectList(new List<SelectListItem>{
                                                new SelectListItem{Text = "1",Value = "1"},
                                                new SelectListItem{Text = "2",Value = "2"},
                                                new SelectListItem{Text = "3",Value = "3"},
                                                new SelectListItem{Text = "4",Value = "4"}
                                                }, "Value", "Text");

            ViewBag.ScoringWeight = new SelectList(new List<SelectListItem>{
                                                new SelectListItem{Text = "1",Value = "1"},
                                                new SelectListItem{Text = "2",Value = "2"},
                                                new SelectListItem{Text = "3",Value = "3"},
                                                new SelectListItem{Text = "4",Value = "4"}
                                                }, "Value", "Text");
            return View();
        }

        // POST: QuestionOptions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]        
        public ActionResult Create([Bind(Include = "QuestionOptionID,QuestionID,OptionID,OptionText,CorrectOption,OptionHint,ExplanationForOption,MisconceptionID,OtherMisconceptionID1,OtherMisconceptionID2,ScoringWeight,Active,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate")] QuestionOption questionOption)
        {
            if (this.db.QuestionOptions.Any((QuestionOption x) => x.QuestionID == questionOption.QuestionID && x.OptionID == questionOption.OptionID))
            {
                base.ModelState.AddModelError("OptionID", "OptionID Already Created for this Question !");
            }
            if (base.ModelState.IsValid)
            {
                this.db.QuestionOptions.Add(questionOption);
                this.db.SaveChanges();
                return base.RedirectToAction("Details", "QuestionMaster", new
                {
                    id = questionOption.QuestionID
                });
            }

            ViewBag.MisconceptionID = new SelectList(db.Misconceptions, "MisconceptionID", "Description", questionOption.MisconceptionID);
            ViewBag.OtherMisconceptionID1 = new SelectList(db.Misconceptions, "MisconceptionID", "Description", questionOption.OtherMisconceptionID1);
            ViewBag.OtherMisconceptionID2 = new SelectList(db.Misconceptions, "MisconceptionID", "Description", questionOption.OtherMisconceptionID2);
            ViewBag.QuestionID = new SelectList(db.QuestionMasters, "QuestionID", "QuestionCode", questionOption.QuestionID);
            ViewBag.OptionID = new SelectList(new List<SelectListItem>{
                                                new SelectListItem{Text = "1",Value = "1"},
                                                new SelectListItem{Text = "2",Value = "2"},
                                                new SelectListItem{Text = "3",Value = "3"},
                                                new SelectListItem{Text = "4",Value = "4"}
                                                }, "Value", "Text", questionOption.OptionID);

            ViewBag.ScoringWeight = new SelectList(new List<SelectListItem>{
                                                new SelectListItem{Text = "1",Value = "1"},
                                                new SelectListItem{Text = "2",Value = "2"},
                                                new SelectListItem{Text = "3",Value = "3"},
                                                new SelectListItem{Text = "4",Value = "4"}
                                                }, "Value", "Text", questionOption.ScoringWeight);

            return View(questionOption);
        }

        // GET: QuestionOptions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionOption questionOption = db.QuestionOptions.Find(id);
            if (questionOption == null)
            {
                return HttpNotFound();
            }
            ViewBag.MisconceptionID = new SelectList(db.Misconceptions, "MisconceptionID", "Description", questionOption.MisconceptionID);
            ViewBag.OtherMisconceptionID1 = new SelectList(db.Misconceptions, "MisconceptionID", "Description", questionOption.OtherMisconceptionID1);
            ViewBag.OtherMisconceptionID2 = new SelectList(db.Misconceptions, "MisconceptionID", "Description", questionOption.OtherMisconceptionID2);
            ViewBag.QuestionID = new SelectList(db.QuestionMasters, "QuestionID", "QuestionCode", questionOption.QuestionID);
            ViewBag.OptionID = new SelectList(new List<SelectListItem>{
                                                new SelectListItem{Text = "1",Value = "1"},
                                                new SelectListItem{Text = "2",Value = "2"},
                                                new SelectListItem{Text = "3",Value = "3"},
                                                new SelectListItem{Text = "4",Value = "4"}
                                                }, "Value", "Text", questionOption.OptionID);

            ViewBag.ScoringWeight = new SelectList(new List<SelectListItem>{
                                                new SelectListItem{Text = "1",Value = "1"},
                                                new SelectListItem{Text = "2",Value = "2"},
                                                new SelectListItem{Text = "3",Value = "3"},
                                                new SelectListItem{Text = "4",Value = "4"}
                                                }, "Value", "Text", questionOption.ScoringWeight);

            return View(questionOption);
        }

        // POST: QuestionOptions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "QuestionOptionID,QuestionID,OptionID,OptionText,CorrectOption,OptionHint,ExplanationForOption,MisconceptionID,OtherMisconceptionID1,OtherMisconceptionID2,ScoringWeight,Active,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate")] QuestionOption questionOption)
        {
            if (ModelState.IsValid)
            {
                db.Entry(questionOption).State = EntityState.Modified;
                db.SaveChanges();
                return base.RedirectToAction("Details", "QuestionMaster", new
                {
                    id = questionOption.QuestionID
                });
            }
            ViewBag.MisconceptionID = new SelectList(db.Misconceptions, "MisconceptionID", "Description", questionOption.MisconceptionID);
            ViewBag.OtherMisconceptionID1 = new SelectList(db.Misconceptions, "MisconceptionID", "Description", questionOption.OtherMisconceptionID1);
            ViewBag.OtherMisconceptionID2 = new SelectList(db.Misconceptions, "MisconceptionID", "Description", questionOption.OtherMisconceptionID2);
            ViewBag.QuestionID = new SelectList(db.QuestionMasters, "QuestionID", "QuestionCode", questionOption.QuestionID);
            ViewBag.OptionID = new SelectList(new List<SelectListItem>{
                                                new SelectListItem{Text = "1",Value = "1"},
                                                new SelectListItem{Text = "2",Value = "2"},
                                                new SelectListItem{Text = "3",Value = "3"},
                                                new SelectListItem{Text = "4",Value = "4"}
                                                }, "Value", "Text", questionOption.OptionID);

            ViewBag.ScoringWeight = new SelectList(new List<SelectListItem>{
                                                new SelectListItem{Text = "1",Value = "1"},
                                                new SelectListItem{Text = "2",Value = "2"},
                                                new SelectListItem{Text = "3",Value = "3"},
                                                new SelectListItem{Text = "4",Value = "4"}
                                                }, "Value", "Text", questionOption.ScoringWeight);

            return View(questionOption);
        }

        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionOption questionOption = this.db.QuestionOptions.Find(new object[]
            {
                id
            });
            if (questionOption == null)
            {
                return base.HttpNotFound();
            }
            return base.View(questionOption);
        }

        [ActionName("Delete"), HttpPost, ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            QuestionOption questionOption = this.db.QuestionOptions.Find(new object[]
            {
                id
            });
            this.db.QuestionOptions.Remove(questionOption);
            this.db.SaveChanges();
            return base.RedirectToAction("Index", new
            {
                QuestionId = questionOption.QuestionID
            });
        }


        public JsonResult IsOptionIdAvailable(int QuestionID, string OptionID)
        {
            return base.Json(!this.db.QuestionOptions.Any((QuestionOption x) => x.QuestionID == QuestionID && x.OptionID == OptionID), JsonRequestBehavior.AllowGet);
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
