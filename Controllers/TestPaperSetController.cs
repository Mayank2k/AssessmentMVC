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
    public class TestPaperSetController : Controller
    {
        private AssesmentContext db = new AssesmentContext();

        // GET: TestPaperSet
        public async Task<ActionResult> Index()
        {
            return View(await db.TestPaperSets.ToListAsync());
        }

        // GET: TestPaperSet/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestPaperSet testPaperSet = await db.TestPaperSets.FindAsync(id);
            if (testPaperSet == null)
            {
                return HttpNotFound();
            }
            return View(testPaperSet);
        }

        // GET: TestPaperSet/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TestPaperSet/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "TestPaperSetCode,Description,Active,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate")] TestPaperSet testPaperSet)
        {
            if (ModelState.IsValid)
            {
                db.TestPaperSets.Add(testPaperSet);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(testPaperSet);
        }

        // GET: TestPaperSet/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestPaperSet testPaperSet = await db.TestPaperSets.FindAsync(id);
            if (testPaperSet == null)
            {
                return HttpNotFound();
            }
            return View(testPaperSet);
        }

        // POST: TestPaperSet/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "TestPaperSetCode,Description,Active,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate")] TestPaperSet testPaperSet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(testPaperSet).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(testPaperSet);
        }

        // GET: TestPaperSet/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestPaperSet testPaperSet = await db.TestPaperSets.FindAsync(id);
            if (testPaperSet == null)
            {
                return HttpNotFound();
            }
            return View(testPaperSet);
        }

        // POST: TestPaperSet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            TestPaperSet testPaperSet = await db.TestPaperSets.FindAsync(id);
            db.TestPaperSets.Remove(testPaperSet);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public JsonResult IsSetCodeAvailable(string testPaperSetCode)
        {
            return base.Json(!db.TestPaperSets.Any((TestPaperSet x) => x.TestPaperSetCode == testPaperSetCode), JsonRequestBehavior.AllowGet);
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
