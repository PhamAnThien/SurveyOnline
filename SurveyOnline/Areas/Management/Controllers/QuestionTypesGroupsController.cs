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
    public class QuestionTypesGroupsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Management/QuestionTypesGroups
        public async Task<ActionResult> Index()
        {
            return View(await db.QuestionTypesGroups.ToListAsync());
        }

        // GET: Management/QuestionTypesGroups/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionTypesGroup questionTypesGroup = await db.QuestionTypesGroups.FindAsync(id);
            if (questionTypesGroup == null)
            {
                return HttpNotFound();
            }
            return View(questionTypesGroup);
        }

        // GET: Management/QuestionTypesGroups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Management/QuestionTypesGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Name")] QuestionTypesGroup questionTypesGroup)
        {
            if (ModelState.IsValid)
            {
                db.QuestionTypesGroups.Add(questionTypesGroup);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(questionTypesGroup);
        }

        // GET: Management/QuestionTypesGroups/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionTypesGroup questionTypesGroup = await db.QuestionTypesGroups.FindAsync(id);
            if (questionTypesGroup == null)
            {
                return HttpNotFound();
            }
            return View(questionTypesGroup);
        }

        // POST: Management/QuestionTypesGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Name")] QuestionTypesGroup questionTypesGroup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(questionTypesGroup).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(questionTypesGroup);
        }

        // GET: Management/QuestionTypesGroups/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionTypesGroup questionTypesGroup = await db.QuestionTypesGroups.FindAsync(id);
            if (questionTypesGroup == null)
            {
                return HttpNotFound();
            }
            return View(questionTypesGroup);
        }

        // POST: Management/QuestionTypesGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            QuestionTypesGroup questionTypesGroup = await db.QuestionTypesGroups.FindAsync(id);
            db.QuestionTypesGroups.Remove(questionTypesGroup);
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
