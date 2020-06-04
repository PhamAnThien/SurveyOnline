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
    public class WorkplaceTypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Management/WorkplaceTypes
        public async Task<ActionResult> Index()
        {
            return View(await db.WorkplaceTypes.ToListAsync());
        }

        // GET: Management/WorkplaceTypes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkplaceType workplaceType = await db.WorkplaceTypes.FindAsync(id);
            if (workplaceType == null)
            {
                return HttpNotFound();
            }
            return View(workplaceType);
        }

        // GET: Management/WorkplaceTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Management/WorkplaceTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Name")] WorkplaceType workplaceType)
        {
            if (ModelState.IsValid)
            {
                db.WorkplaceTypes.Add(workplaceType);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(workplaceType);
        }

        // GET: Management/WorkplaceTypes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkplaceType workplaceType = await db.WorkplaceTypes.FindAsync(id);
            if (workplaceType == null)
            {
                return HttpNotFound();
            }
            return View(workplaceType);
        }

        // POST: Management/WorkplaceTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Name")] WorkplaceType workplaceType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(workplaceType).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(workplaceType);
        }

        // GET: Management/WorkplaceTypes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkplaceType workplaceType = await db.WorkplaceTypes.FindAsync(id);
            if (workplaceType == null)
            {
                return HttpNotFound();
            }
            return View(workplaceType);
        }

        // POST: Management/WorkplaceTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            WorkplaceType workplaceType = await db.WorkplaceTypes.FindAsync(id);
            db.WorkplaceTypes.Remove(workplaceType);
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
