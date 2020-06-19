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
    [Authorize]
    public class WorkplacesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Management/Workplaces
        public async Task<ActionResult> Index()
        {
            var workplaces = db.Workplaces.Include(w => w.WorkplaceType);
            return View(await workplaces.ToListAsync());
        }

        // GET: Management/Workplaces/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Workplace workplace = await db.Workplaces.FindAsync(id);
            if (workplace == null)
            {
                return HttpNotFound();
            }
            return View(workplace);
        }

        // GET: Management/Workplaces/Create
        public ActionResult Create()
        {
            ViewBag.WorkplaceTypeID = new SelectList(db.WorkplaceTypes, "ID", "Name");
            return View();
        }

        // POST: Management/Workplaces/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Name,WorkplaceTypeID,Address,PhoneNumber,Email,TaxCode,ManagerName")] Workplace workplace)
        {
            if (ModelState.IsValid)
            {
                db.Workplaces.Add(workplace);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.WorkplaceTypeID = new SelectList(db.WorkplaceTypes, "ID", "Name", workplace.WorkplaceTypeID);
            return View(workplace);
        }

        // GET: Management/Workplaces/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Workplace workplace = await db.Workplaces.FindAsync(id);
            if (workplace == null)
            {
                return HttpNotFound();
            }
            ViewBag.WorkplaceTypeID = new SelectList(db.WorkplaceTypes, "ID", "Name", workplace.WorkplaceTypeID);
            return View(workplace);
        }

        // POST: Management/Workplaces/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Name,WorkplaceTypeID,Address,PhoneNumber,Email,TaxCode,ManagerName")] Workplace workplace)
        {
            if (ModelState.IsValid)
            {
                db.Entry(workplace).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.WorkplaceTypeID = new SelectList(db.WorkplaceTypes, "ID", "Name", workplace.WorkplaceTypeID);
            return View(workplace);
        }

        // GET: Management/Workplaces/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Workplace workplace = await db.Workplaces.FindAsync(id);
            if (workplace == null)
            {
                return HttpNotFound();
            }
            return View(workplace);
        }

        // POST: Management/Workplaces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Workplace workplace = await db.Workplaces.FindAsync(id);
            db.Workplaces.Remove(workplace);
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
