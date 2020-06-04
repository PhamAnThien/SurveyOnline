using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SurveyOnline.Models;

namespace SurveyOnline.Areas.Management.Controllers
{
    public class BranchesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Management/Branches
        public async Task<ActionResult> Index()
        {
            var branches = db.Branches.Include(b => b.Workplace);
            return View(await branches.ToListAsync());
        }

        // GET: Management/Branches/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Branch branch = await db.Branches.FindAsync(id);
            if (branch == null)
            {
                return HttpNotFound();
            }
            return View(branch);
        }

        // GET: Management/Branches/Create
        public ActionResult Create()
        {
            ViewBag.WorkplaceID = new SelectList(db.Workplaces, "ID", "Name");
            return View();
        }

        // POST: Management/Branches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Name,WorkplaceID")] Branch branch)
        {
            if (ModelState.IsValid)
            {
                db.Branches.Add(branch);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.WorkplaceID = new SelectList(db.Workplaces, "ID", "Name", branch.WorkplaceID);
            return View(branch);
        }

        // GET: Management/Branches/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Branch branch = await db.Branches.FindAsync(id);
            if (branch == null)
            {
                return HttpNotFound();
            }
            ViewBag.WorkplaceID = new SelectList(db.Workplaces, "ID", "Name", branch.WorkplaceID);
            return View(branch);
        }

        // POST: Management/Branches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Name,WorkplaceID")] Branch branch)
        {
            if (ModelState.IsValid)
            {
                db.Entry(branch).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.WorkplaceID = new SelectList(db.Workplaces, "ID", "Name", branch.WorkplaceID);
            return View(branch);
        }

        // GET: Management/Branches/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Branch branch = await db.Branches.FindAsync(id);
            if (branch == null)
            {
                return HttpNotFound();
            }
            return View(branch);
        }

        // POST: Management/Branches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Branch branch = await db.Branches.FindAsync(id);
            db.Branches.Remove(branch);
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
