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
    public class QuestionTagsController : Controller
    {
        private AssesmentContext db = new AssesmentContext();

        // GET: QuestionTags
        //public ActionResult Index()
        //{
        //    var questionTags = db.QuestionTags.Include(q => q.Chapter).Include(q => q.Competency).Include(q => q.Course).Include(q => q.DifficultyLevel).Include(q => q.LearningLevel).Include(q => q.LearningObjective).Include(q => q.LearningOutcome).Include(q => q.QuestionMaster).Include(q => q.Skill).Include(q => q.SubSkill).Include(q => q.SubTopic).Include(q => q.Topic).Include(q => q.Unit);
        //    return View(questionTags.ToList());
        //}

        public ActionResult Index(int questionId, string questionCode)
        {
            IQueryable<QuestionTag> source = (from x in this.db.QuestionTags
                                              where x.QuestionID == questionId
                                              select x).Include((QuestionTag q) => q.Chapter).Include((QuestionTag q) => q.Competency).Include((QuestionTag q) => q.Course).Include((QuestionTag q) => q.DifficultyLevel).Include((QuestionTag q) => q.LearningLevel).Include((QuestionTag q) => q.LearningObjective).Include((QuestionTag q) => q.LearningOutcome).Include((QuestionTag q) => q.QuestionMaster).Include((QuestionTag q) => q.Skill).Include((QuestionTag q) => q.SubSkill).Include((QuestionTag q) => q.SubTopic).Include((QuestionTag q) => q.Topic).Include((QuestionTag q) => q.Unit);
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
            return base.View(source.ToList<QuestionTag>());
        }


        // GET: QuestionTags/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionTag questionTag = db.QuestionTags.Find(id);
            if (questionTag == null)
            {
                return HttpNotFound();
            }
            return View(questionTag);
        }

        // GET: QuestionTags/Create
        public ActionResult Create(int questionId, string questionCode)
        {
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
            ViewBag.ChapterID = new SelectList(db.Chapters, "ChapterID", "Description");
            ViewBag.CompetencyID = new SelectList(db.Competencies, "CompetencyID", "Description");
            ViewBag.CourseID = new SelectList(db.Courses.Where(x => x.Active), "CourseID", "CourseName");
            ViewBag.DifficultyLevelID = new SelectList(db.DifficultyLevels, "DifficultyLevelID", "Description");
            ViewBag.LearningLevelID = new SelectList(db.LearningLevels, "LearningLevelID", "Description");
            ViewBag.LearningObjectiveID = new SelectList(db.LearningObjectives, "LearningObjectiveID", "Description");
            ViewBag.LearningOutcomeID = new SelectList(db.LearningOutcomes, "LearningOutcomeID", "Description");
            //ViewBag.QuestionID = new SelectList(db.QuestionMasters, "QuestionID", "QuestionCode");
            ViewBag.SkillID = new SelectList(db.Skills, "SkillID", "Description");
            ViewBag.SubskillID = new SelectList(db.SubSkills, "SubSkillID", "Description");
            ViewBag.SubTopicID = new SelectList(db.SubTopics, "SubTopicID", "Description");
            ViewBag.TopicID = new SelectList(db.Topics, "TopicID", "Description");
            ViewBag.UnitID = new SelectList(db.Units, "UnitID", "Description");
            return View();
        }

        // POST: QuestionTags/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "QuestionTagID,QuestionID,CourseID,UnitID,ChapterID,TopicID,SubTopicID,LearningObjectiveID,LearningOutcomeID,LearningLevelID,DifficultyLevelID,CompetencyID,SkillID,SubskillID,Active,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate")] QuestionTag questionTag)
        {
            if (ModelState.IsValid)
            {
                db.QuestionTags.Add(questionTag);
                db.SaveChanges();
                return base.RedirectToAction("Details", "QuestionMaster", new
                {
                    id = questionTag.QuestionID
                });
            }

            ViewBag.QuestionID = new SelectList(db.QuestionMasters, "QuestionID", "QuestionCode", questionTag.QuestionID);
            ViewBag.CourseID = new SelectList(db.Courses.Where(x => x.Active), "CourseID", "CourseName", questionTag.CourseID);
            //ViewBag.ChapterID = new SelectList(db.Chapters, "ChapterID", "Description", questionTag.ChapterID);
            ViewBag.UnitID = new SelectList(db.Units.Where(x => x.CourseID == questionTag.CourseID), "UnitID", "Description", questionTag.UnitID);
            ViewBag.TopicID = new SelectList(db.Topics.Where(x => x.UnitID == questionTag.UnitID), "TopicID", "Description", questionTag.TopicID);
            ViewBag.SubTopicID = new SelectList(db.SubTopics.Where(x => x.TopicID == questionTag.TopicID), "SubTopicID", "Description", questionTag.SubTopicID);
            ViewBag.LearningObjectiveID = new SelectList(db.LearningObjectives.Where(x => x.TopicID == questionTag.TopicID), "LearningObjectiveID", "Description", questionTag.LearningObjectiveID);
            ViewBag.LearningOutcomeID = new SelectList(db.LearningOutcomes.Where(x => x.TopicID == questionTag.TopicID), "LearningOutcomeID", "Description", questionTag.LearningOutcomeID);

            ViewBag.CompetencyID = new SelectList(db.Competencies, "CompetencyID", "Description", questionTag.CompetencyID);
            ViewBag.SkillID = new SelectList(db.Skills.Where(x => x.CompetencyID == questionTag.CompetencyID), "SkillID", "Description", questionTag.SkillID);
            ViewBag.SubskillID = new SelectList(db.SubSkills.Where(x => x.SkillID == questionTag.SkillID), "SubSkillID", "Description", questionTag.SubskillID);

            ViewBag.DifficultyLevelID = new SelectList(db.DifficultyLevels, "DifficultyLevelID", "Description", questionTag.DifficultyLevelID);
            ViewBag.LearningLevelID = new SelectList(db.LearningLevels, "LearningLevelID", "Description", questionTag.LearningLevelID);
            
            return View(questionTag);
        }

        // GET: QuestionTags/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionTag questionTag = db.QuestionTags.Find(id);
            if (questionTag == null)
            {
                return HttpNotFound();
            }
            ViewBag.QuestionID = new SelectList(db.QuestionMasters, "QuestionID", "QuestionCode", questionTag.QuestionID);
            ViewBag.CourseID = new SelectList(db.Courses.Where(x => x.Active), "CourseID", "CourseName", questionTag.CourseID);
            //ViewBag.ChapterID = new SelectList(db.Chapters, "ChapterID", "Description", questionTag.ChapterID);
            ViewBag.UnitID = new SelectList(db.Units.Where(x => x.CourseID == questionTag.CourseID), "UnitID", "Description", questionTag.UnitID);
            ViewBag.TopicID = new SelectList(db.Topics.Where(x => x.UnitID == questionTag.UnitID), "TopicID", "Description", questionTag.TopicID);
            ViewBag.SubTopicID = new SelectList(db.SubTopics.Where(x => x.TopicID == questionTag.TopicID), "SubTopicID", "Description", questionTag.SubTopicID);
            ViewBag.LearningObjectiveID = new SelectList(db.LearningObjectives.Where(x => x.TopicID == questionTag.TopicID), "LearningObjectiveID", "Description", questionTag.LearningObjectiveID);
            ViewBag.LearningOutcomeID = new SelectList(db.LearningOutcomes.Where(x => x.TopicID == questionTag.TopicID), "LearningOutcomeID", "Description", questionTag.LearningOutcomeID);

            ViewBag.CompetencyID = new SelectList(db.Competencies, "CompetencyID", "Description", questionTag.CompetencyID);
            ViewBag.SkillID = new SelectList(db.Skills.Where(x => x.CompetencyID == questionTag.CompetencyID), "SkillID", "Description", questionTag.SkillID);
            ViewBag.SubskillID = new SelectList(db.SubSkills.Where(x => x.SkillID == questionTag.SkillID), "SubSkillID", "Description", questionTag.SubskillID);

            ViewBag.DifficultyLevelID = new SelectList(db.DifficultyLevels, "DifficultyLevelID", "Description", questionTag.DifficultyLevelID);
            ViewBag.LearningLevelID = new SelectList(db.LearningLevels, "LearningLevelID", "Description", questionTag.LearningLevelID);

            return View(questionTag);
        }

        // POST: QuestionTags/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "QuestionTagID,QuestionID,CourseID,UnitID,ChapterID,TopicID,SubTopicID,LearningObjectiveID,LearningOutcomeID,LearningLevelID,DifficultyLevelID,CompetencyID,SkillID,SubskillID,Active,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate")] QuestionTag questionTag)
        {
            if (ModelState.IsValid)
            {
                db.Entry(questionTag).State = EntityState.Modified;
                db.SaveChanges();
                return base.RedirectToAction("Details", "QuestionMaster", new
                {
                    id = questionTag.QuestionID
                });
            }
            ViewBag.QuestionID = new SelectList(db.QuestionMasters, "QuestionID", "QuestionCode", questionTag.QuestionID);
            ViewBag.CourseID = new SelectList(db.Courses.Where(x => x.Active), "CourseID", "CourseName", questionTag.CourseID);
            //ViewBag.ChapterID = new SelectList(db.Chapters, "ChapterID", "Description", questionTag.ChapterID);
            ViewBag.UnitID = new SelectList(db.Units.Where(x => x.CourseID == questionTag.CourseID), "UnitID", "Description", questionTag.UnitID);
            ViewBag.TopicID = new SelectList(db.Topics.Where(x => x.UnitID == questionTag.UnitID), "TopicID", "Description", questionTag.TopicID);
            ViewBag.SubTopicID = new SelectList(db.SubTopics.Where(x => x.TopicID == questionTag.TopicID), "SubTopicID", "Description", questionTag.SubTopicID);
            ViewBag.LearningObjectiveID = new SelectList(db.LearningObjectives.Where(x => x.TopicID == questionTag.TopicID), "LearningObjectiveID", "Description", questionTag.LearningObjectiveID);
            ViewBag.LearningOutcomeID = new SelectList(db.LearningOutcomes.Where(x => x.TopicID == questionTag.TopicID), "LearningOutcomeID", "Description", questionTag.LearningOutcomeID);

            ViewBag.CompetencyID = new SelectList(db.Competencies, "CompetencyID", "Description", questionTag.CompetencyID);
            ViewBag.SkillID = new SelectList(db.Skills.Where(x => x.CompetencyID == questionTag.CompetencyID), "SkillID", "Description", questionTag.SkillID);
            ViewBag.SubskillID = new SelectList(db.SubSkills.Where(x => x.SkillID == questionTag.SkillID), "SubSkillID", "Description", questionTag.SubskillID);

            ViewBag.DifficultyLevelID = new SelectList(db.DifficultyLevels, "DifficultyLevelID", "Description", questionTag.DifficultyLevelID);
            ViewBag.LearningLevelID = new SelectList(db.LearningLevels, "LearningLevelID", "Description", questionTag.LearningLevelID);
            return View(questionTag);
        }

        // GET: QuestionTags/Delete/5
        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionTag questionTag = this.db.QuestionTags.Find(new object[]
            {
                id
            });
            if (questionTag == null)
            {
                return base.HttpNotFound();
            }
            return base.View(questionTag);
        }

        [ActionName("Delete"), HttpPost, ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            QuestionTag questionTag = this.db.QuestionTags.Find(new object[]
            {
                id
            });
            this.db.QuestionTags.Remove(questionTag);
            this.db.SaveChanges();
            return base.RedirectToAction("Index", new
            {
                QuestionId = questionTag.QuestionID
            });
        }

        public JsonResult GetSkills(int competencyId)
        {
            return base.Json(from x in this.db.Skills
                             where x.CompetencyID == competencyId
                             select x into s
                             select new
                             {
                                 s.SkillID,
                                 s.Description
                             }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSubSkills(int skillId)
        {
            return base.Json(from x in this.db.SubSkills
                             where x.SkillID == skillId
                             select x into s
                             select new
                             {
                                 s.SubSkillID,
                                 s.Description
                             }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUnits(int courseId)
        {
            return base.Json(from x in this.db.Units
                             where x.CourseID == courseId
                             select x into s
                             select new
                             {
                                 s.UnitID,
                                 s.Description
                             }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTopics(int unitId)
        {
            return base.Json(from x in this.db.Topics
                             where x.UnitID == unitId
                             select x into s
                             select new
                             {
                                 s.TopicID,
                                 s.Description
                             }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSubTopics(int topicId)
        {
            return base.Json(from x in this.db.SubTopics
                             where x.TopicID == topicId
                             select x into s
                             select new
                             {
                                 s.SubTopicID,
                                 s.Description
                             }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetLearningObjectives(int topicId)
        {
            return base.Json(from x in this.db.LearningObjectives
                             where x.TopicID == topicId
                             select x into s
                             select new
                             {
                                 s.LearningObjectiveID,
                                 s.Description
                             }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetLearningOutcomes(int topicId)
        {
            return base.Json(from x in this.db.LearningOutcomes
                             where x.TopicID == topicId
                             select x into s
                             select new
                             {
                                 s.LearningOutcomeID,
                                 s.Description
                             }, JsonRequestBehavior.AllowGet);
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
