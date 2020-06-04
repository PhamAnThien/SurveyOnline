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
    public class SurveyGroupsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Management/SurveyGroups
        public async Task<ActionResult> Index()
        {
            var surveyGroups = db.SurveyGroups.Include(s => s.SurveySubject);
            return View(await surveyGroups.ToListAsync());
        }

        // GET: Management/SurveyGroups/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SurveyGroup surveyGroup = await db.SurveyGroups.FindAsync(id);
            if (surveyGroup == null)
            {
                return HttpNotFound();
            }
            return View(surveyGroup);
        }

        // GET: Management/SurveyGroups/Create
        public ActionResult Create(int? SubjectID, string returnURL)
        {
            if (SubjectID != null)
            {
                ViewBag.SurveySubjectId = new SelectList(db.SurveySubjects.Where(s=>s.ID == SubjectID), "ID", "SubjectName");
            }
            else
                ViewBag.SurveySubjectId = new SelectList(db.SurveySubjects, "ID", "SubjectName");
            ViewBag.returnURL = returnURL;
            return View();
        }

        // POST: Management/SurveyGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Name,SurveySubjectId")] SurveyGroup surveyGroup, string returnURL)
        {
            if (ModelState.IsValid)
            {
                db.SurveyGroups.Add(surveyGroup);
                await db.SaveChangesAsync();
                if (!string.IsNullOrEmpty(returnURL))
                {
                    return Redirect(returnURL);
                }
                return RedirectToAction("Index");
            }

            ViewBag.SurveySubjectId = new SelectList(db.SurveySubjects, "ID", "SubjectName", surveyGroup.SurveySubjectId);
            return View(surveyGroup);
        }

        // GET: Management/SurveyGroups/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SurveyGroup surveyGroup = await db.SurveyGroups.FindAsync(id);
            if (surveyGroup == null)
            {
                return HttpNotFound();
            }
            ViewBag.SurveySubjectId = new SelectList(db.SurveySubjects, "ID", "SubjectName", surveyGroup.SurveySubjectId);
            return View(surveyGroup);
        }

        // POST: Management/SurveyGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Name,SurveySubjectId")] SurveyGroup surveyGroup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(surveyGroup).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.SurveySubjectId = new SelectList(db.SurveySubjects, "ID", "SubjectName", surveyGroup.SurveySubjectId);
            return View(surveyGroup);
        }

        // GET: Management/SurveyGroups/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SurveyGroup surveyGroup = await db.SurveyGroups.FindAsync(id);
            if (surveyGroup == null)
            {
                return HttpNotFound();
            }
            return View(surveyGroup);
        }

        // POST: Management/SurveyGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            SurveyGroup surveyGroup = await db.SurveyGroups.FindAsync(id);
            db.SurveyGroups.Remove(surveyGroup);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<ActionResult> getListSurveyGroup(int SubjectID)
        {
            var SurveySubjectGroupId = new SelectList(await db.SurveyGroups.Where(s => s.SurveySubjectId == SubjectID).ToListAsync(), "ID", "Name");

            return Json(SurveySubjectGroupId, JsonRequestBehavior.AllowGet);
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
