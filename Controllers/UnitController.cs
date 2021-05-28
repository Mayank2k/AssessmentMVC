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
    public class UnitController : Controller
    {
        private AssesmentContext db = new AssesmentContext();

        // GET: Units
        public async Task<ActionResult> Index(string unit, int? courseID)
        {
            IQueryable<Unit> units;
                        
            if (courseID != null)
            {
                ViewBag.CourseID = new SelectList(db.Courses.Where(x => x.Active && x.CourseID > 0), "CourseID", "CourseName", courseID);
                units = db.Units.Where(x=>x.CourseID == courseID).Include(u => u.Course);
            }
            else
            {
                ViewBag.CourseID = new SelectList(db.Courses.Where(x => x.Active && x.CourseID > 0), "CourseID", "CourseName");
                units = db.Units.Where(x => x.Active && (x.Description.StartsWith(unit) || unit == null)).Include(u => u.Course);                                
            }
            
            return View(await units.OrderBy(x=>x.CourseID).ToListAsync());
        }

        // GET: Units/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Unit unit = await db.Units.FindAsync(id);
            if (unit == null)
            {
                return HttpNotFound();
            }
            return View(unit);
        }

        // GET: Units/Create
        public ActionResult Create(int? courseID)
        {
            ViewBag.CourseID = new SelectList(db.Courses.Where(x => x.Active && x.CourseID > 0), "CourseID", "CourseName", courseID);
            return View();
        }

        // POST: Units/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "UnitID,Description,CourseID,Active,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate")] Unit unit)
        {
            if (ModelState.IsValid)
            {
                db.Units.Add(unit);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CourseID = new SelectList(db.Courses.Where(x => x.Active && x.CourseID > 0), "CourseID", "CourseName", unit.CourseID);            
            return View(unit);
        }

        // GET: Units/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Unit unit = await db.Units.FindAsync(id);
            if (unit == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseID = new SelectList(db.Courses.Where(x => x.Active && x.CourseID > 0), "CourseID", "CourseName", unit.CourseID);
            return View(unit);
        }

        // POST: Units/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "UnitID,Description,CourseID,Active,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate")] Unit unit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(unit).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CourseID = new SelectList(db.Courses.Where(x => x.Active && x.CourseID > 0), "CourseID", "CourseName", unit.CourseID);
            return View(unit);
        }

        // GET: Units/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Unit unit = await db.Units.FindAsync(id);
            if (unit == null)
            {
                return HttpNotFound();
            }
            return View(unit);
        }

        // POST: Units/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Unit unit = await db.Units.FindAsync(id);
            db.Units.Remove(unit);
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
