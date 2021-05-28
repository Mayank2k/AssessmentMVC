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
    public class TopicController : Controller
    {
        private AssesmentContext db = new AssesmentContext();

        // GET: Topic
        public async Task<ActionResult> Index(int UnitId)
        {
            return View(await db.Topics.Where(x=>x.UnitID==UnitId).ToListAsync());
        }

        // GET: Topic/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = await db.Topics.FindAsync(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }

        // GET: Topic/Create
        public ActionResult Create(int? courseID, int unitID)
        {
            if (courseID==null)
            {
                courseID = db.Units.Where(x => x.UnitID == unitID).First().CourseID;
            }
            ViewBag.CourseID = new SelectList(db.Courses.Where(x => x.Active && x.CourseID > 0), "CourseID", "CourseName", courseID);
            ViewBag.UnitID = new SelectList(db.Units.Where(x => x.Active && x.CourseID > 0), "UnitID", "Description", unitID);
            return View();
        }

        // POST: Topic/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "TopicID,Description,CourseID,UnitID,ChapterID,Active,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate")] Topic topic)
        {
            if (ModelState.IsValid)
            {
                if (topic.CourseID <= 0)
                {
                    topic.CourseID = db.Units.Where(x => x.UnitID == topic.UnitID).First().CourseID;
                }
                db.Topics.Add(topic);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", new { unitID = topic.UnitID });
            }
            ViewBag.CourseID = new SelectList(db.Courses.Where(x => x.Active && x.CourseID > 0), "CourseID", "CourseName", topic.CourseID);
            ViewBag.UnitID = new SelectList(db.Units.Where(x => x.Active && x.CourseID > 0), "UnitID", "Description", topic.UnitID);

            return View(topic);
        }

        // GET: Topic/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = await db.Topics.FindAsync(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseID = new SelectList(db.Courses.Where(x => x.Active && x.CourseID > 0), "CourseID", "CourseName", topic.CourseID);
            ViewBag.UnitID = new SelectList(db.Units.Where(x => x.Active && x.CourseID > 0), "UnitID", "Description", topic.UnitID);

            return View(topic);
        }

        // POST: Topic/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "TopicID,Description,CourseID,UnitID,ChapterID,Active,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate")] Topic topic)
        {
            if (ModelState.IsValid)
            {
                db.Entry(topic).State = EntityState.Modified;
                await db.SaveChangesAsync();                
                return RedirectToAction("Index", new { unitID = topic.UnitID });
            }
            ViewBag.CourseID = new SelectList(db.Courses.Where(x => x.Active && x.CourseID > 0), "CourseID", "CourseName", topic.CourseID);
            ViewBag.UnitID = new SelectList(db.Units.Where(x => x.Active && x.CourseID > 0), "UnitID", "Description", topic.UnitID);

            return View(topic);
        }

        // GET: Topic/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = await db.Topics.FindAsync(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }

        // POST: Topic/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Topic topic = await db.Topics.FindAsync(id);
            db.Topics.Remove(topic);
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
