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
    public class LearningOutcomeController : Controller
    {
        private AssesmentContext db = new AssesmentContext();

        // GET: LearningOutcome
        public async Task<ActionResult> Index(int TopicId)
        {
            return View(await db.LearningOutcomes.Where(x=>x.TopicID == TopicId).ToListAsync());
        }

        // GET: LearningOutcome/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LearningOutcome learningOutcome = await db.LearningOutcomes.FindAsync(id);
            if (learningOutcome == null)
            {
                return HttpNotFound();
            }
            return View(learningOutcome);
        }

        // GET: LearningOutcome/Create
        public ActionResult Create(int topicID)
        {
            int unitID = db.Topics.Where(x => x.Active && x.TopicID == topicID).Single().UnitID;
            int courseID = db.Topics.Where(x => x.Active && x.TopicID == topicID).Single().CourseID;
            ViewBag.TopicID = new SelectList(db.Topics.Where(x => x.Active), "TopicID", "Description", topicID);
            ViewBag.UnitID = new SelectList(db.Units.Where(x => x.Active && x.CourseID > 0), "UnitID", "Description", unitID);
            ViewBag.CourseID = new SelectList(db.Courses.Where(x => x.Active && x.CourseID > 0), "CourseID", "CourseName", courseID);
            return View();
        }

        // POST: LearningOutcome/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "LearningOutcomeID,Description,TopicID,Active,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate")] LearningOutcome learningOutcome)
        {
            if (ModelState.IsValid)
            {
                db.LearningOutcomes.Add(learningOutcome);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", new { topicId = learningOutcome.TopicID });
            }
            int unitID = db.Topics.Where(x => x.Active && x.TopicID == learningOutcome.TopicID).Single().UnitID;
            int courseID = db.Topics.Where(x => x.Active && x.TopicID == learningOutcome.TopicID).Single().CourseID;
            ViewBag.TopicID = new SelectList(db.Topics.Where(x => x.Active), "TopicID", "Description", learningOutcome.TopicID);
            ViewBag.UnitID = new SelectList(db.Units.Where(x => x.Active && x.CourseID > 0), "UnitID", "Description", unitID);
            ViewBag.CourseID = new SelectList(db.Courses.Where(x => x.Active && x.CourseID > 0), "CourseID", "CourseName", courseID);
            return View(learningOutcome);
        }

        // GET: LearningOutcome/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LearningOutcome learningOutcome = await db.LearningOutcomes.FindAsync(id);
            if (learningOutcome == null)
            {
                return HttpNotFound();
            }
            int unitID = db.Topics.Where(x => x.Active && x.TopicID == learningOutcome.TopicID).Single().UnitID;
            int courseID = db.Topics.Where(x => x.Active && x.TopicID == learningOutcome.TopicID).Single().CourseID;
            ViewBag.TopicID = new SelectList(db.Topics.Where(x => x.Active), "TopicID", "Description", learningOutcome.TopicID);
            ViewBag.UnitID = new SelectList(db.Units.Where(x => x.Active && x.CourseID > 0), "UnitID", "Description", unitID);
            ViewBag.CourseID = new SelectList(db.Courses.Where(x => x.Active && x.CourseID > 0), "CourseID", "CourseName", courseID);
            return View(learningOutcome);
        }

        // POST: LearningOutcome/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "LearningOutcomeID,Description,TopicID,Active,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate")] LearningOutcome learningOutcome)
        {
            if (ModelState.IsValid)
            {
                db.Entry(learningOutcome).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index", new { topicId = learningOutcome.TopicID });
            }
            int unitID = db.Topics.Where(x => x.Active && x.TopicID == learningOutcome.TopicID).Single().UnitID;
            int courseID = db.Topics.Where(x => x.Active && x.TopicID == learningOutcome.TopicID).Single().CourseID;
            ViewBag.TopicID = new SelectList(db.Topics.Where(x => x.Active), "TopicID", "Description", learningOutcome.TopicID);
            ViewBag.UnitID = new SelectList(db.Units.Where(x => x.Active && x.CourseID > 0), "UnitID", "Description", unitID);
            ViewBag.CourseID = new SelectList(db.Courses.Where(x => x.Active && x.CourseID > 0), "CourseID", "CourseName", courseID);
            return View(learningOutcome);
        }

        // GET: LearningOutcome/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LearningOutcome learningOutcome = await db.LearningOutcomes.FindAsync(id);
            if (learningOutcome == null)
            {
                return HttpNotFound();
            }
            return View(learningOutcome);
        }

        // POST: LearningOutcome/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            LearningOutcome learningOutcome = await db.LearningOutcomes.FindAsync(id);
            db.LearningOutcomes.Remove(learningOutcome);
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
