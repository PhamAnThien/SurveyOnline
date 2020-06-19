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
    public class SurveySubjectsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Management/SurveySubjects
        public async Task<ActionResult> Index()
        {
            return View(await db.SurveySubjects.ToListAsync());
        }

        // GET: Management/SurveySubjects/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SurveySubject surveySubject = await db.SurveySubjects.FindAsync(id);
            if (surveySubject == null)
            {
                return HttpNotFound();
            }
            return View(surveySubject);
        }

        // GET: Management/SurveySubjects/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Management/SurveySubjects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,SubjectName,Description,DescriptionFooter")] SurveySubject surveySubject)
        {
            if (ModelState.IsValid)
            {
                db.SurveySubjects.Add(surveySubject);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(surveySubject);
        }

        // GET: Management/SurveySubjects/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SurveySubject surveySubject = await db.SurveySubjects.FindAsync(id);
            if (surveySubject == null)
            {
                return HttpNotFound();
            }
            return View(surveySubject);
        }

        // POST: Management/SurveySubjects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,SubjectName,Description,DescriptionFooter")] SurveySubject surveySubject)
        {
            if (ModelState.IsValid)
            {
                db.Entry(surveySubject).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(surveySubject);
        }

        // GET: Management/SurveySubjects/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SurveySubject surveySubject = await db.SurveySubjects.FindAsync(id);
            if (surveySubject == null)
            {
                return HttpNotFound();
            }
            return View(surveySubject);
        }

        // POST: Management/SurveySubjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            SurveySubject surveySubject = await db.SurveySubjects.FindAsync(id);
            db.SurveySubjects.Remove(surveySubject);
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
