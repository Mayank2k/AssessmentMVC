using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AssessmentMVC.Models;

namespace AssessmentMVC.Controllers
{
    [Authorize]
    public class LearningObjectiveController : Controller
    {
        private AssesmentContext db = new AssesmentContext();

        // GET: LearningObjective
        public async Task<ActionResult> Index(int TopicId)
        {
            return View(await db.LearningObjectives.Where(x => x.TopicID == TopicId).ToListAsync());
        }

        // GET: LearningObjective/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LearningObjective learningObjective = await db.LearningObjectives.FindAsync(id);
            if (learningObjective == null)
            {
                return HttpNotFound();
            }
            return View(learningObjective);
        }

        // GET: LearningObjective/Create
        public ActionResult Create(int topicID)
        {
            ViewBag.TopicID = new SelectList(db.Topics.Where(x => x.Active), "TopicID", "Description", topicID);
            int unitID = db.Topics.Where(x => x.Active && x.TopicID == topicID).Single().UnitID;
            int courseID = db.Topics.Where(x => x.Active && x.TopicID == topicID).Single().CourseID;
            ViewBag.UnitID = new SelectList(db.Units.Where(x => x.Active && x.CourseID > 0), "UnitID", "Description", unitID);
            ViewBag.CourseID = new SelectList(db.Courses.Where(x => x.Active && x.CourseID > 0), "CourseID", "CourseName", courseID);

            return View();
        }

        // POST: LearningObjective/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "LearningObjectiveID,Description,TopicID,Active,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate")] LearningObjective learningObjective)
        {
            if (ModelState.IsValid)
            {
                db.LearningObjectives.Add(learningObjective);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", new { topicId = learningObjective.TopicID });
            }

            int unitID = db.Topics.Where(x => x.Active && x.TopicID == learningObjective.TopicID).Single().UnitID;
            int courseID = db.Topics.Where(x => x.Active && x.TopicID == learningObjective.TopicID).Single().CourseID;
            ViewBag.TopicID = new SelectList(db.Topics.Where(x => x.Active), "TopicID", "Description", learningObjective.TopicID);
            ViewBag.UnitID = new SelectList(db.Units.Where(x => x.Active && x.CourseID > 0), "UnitID", "Description", unitID);
            ViewBag.CourseID = new SelectList(db.Courses.Where(x => x.Active && x.CourseID > 0), "CourseID", "CourseName", courseID);

            return View(learningObjective);
        }

        // GET: LearningObjective/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LearningObjective learningObjective = await db.LearningObjectives.FindAsync(id);
            if (learningObjective == null)
            {
                return HttpNotFound();
            }
            int unitID = db.Topics.Where(x => x.Active && x.TopicID == learningObjective.TopicID).Single().UnitID;
            int courseID = db.Topics.Where(x => x.Active && x.TopicID == learningObjective.TopicID).Single().CourseID;
            ViewBag.TopicID = new SelectList(db.Topics.Where(x => x.Active), "TopicID", "Description", learningObjective.TopicID);
            ViewBag.UnitID = new SelectList(db.Units.Where(x => x.Active && x.CourseID > 0), "UnitID", "Description", unitID);
            ViewBag.CourseID = new SelectList(db.Courses.Where(x => x.Active && x.CourseID > 0), "CourseID", "CourseName", courseID);
            return View(learningObjective);
        }

        // POST: LearningObjective/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "LearningObjectiveID,Description,TopicID,Active,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate")] LearningObjective learningObjective)
        {
            if (ModelState.IsValid)
            {
                db.Entry(learningObjective).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index", new { topicId = learningObjective.TopicID });
            }
            int unitID = db.Topics.Where(x => x.Active && x.TopicID == learningObjective.TopicID).Single().UnitID;
            int courseID = db.Topics.Where(x => x.Active && x.TopicID == learningObjective.TopicID).Single().CourseID;
            ViewBag.TopicID = new SelectList(db.Topics.Where(x => x.Active), "TopicID", "Description", learningObjective.TopicID);
            ViewBag.UnitID = new SelectList(db.Units.Where(x => x.Active && x.CourseID > 0), "UnitID", "Description", unitID);
            ViewBag.CourseID = new SelectList(db.Courses.Where(x => x.Active && x.CourseID > 0), "CourseID", "CourseName", courseID);
            return View(learningObjective);
        }

        // GET: LearningObjective/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LearningObjective learningObjective = await db.LearningObjectives.FindAsync(id);
            if (learningObjective == null)
            {
                return HttpNotFound();
            }
            return View(learningObjective);
        }

        // POST: LearningObjective/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            LearningObjective learningObjective = await db.LearningObjectives.FindAsync(id);
            db.LearningObjectives.Remove(learningObjective);
            await db.SaveChangesAsync();
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
