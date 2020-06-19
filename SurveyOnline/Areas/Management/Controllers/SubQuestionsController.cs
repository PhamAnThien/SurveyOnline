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
    public class SubQuestionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Management/SubQuestions
        public async Task<ActionResult> Index()
        {
            var subQuestions = db.SubQuestions.Include(s => s.QuestionSurvey);
            return View(await subQuestions.ToListAsync());
        }

        // GET: Management/SubQuestions/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubQuestion subQuestion = await db.SubQuestions.FindAsync(id);
            if (subQuestion == null)
            {
                return HttpNotFound();
            }
            return View(subQuestion);
        }

        // GET: Management/SubQuestions/Create
        public ActionResult Create(int? QuestionID, string returnURL)
        {
            if (QuestionID != null)
            {
                ViewBag.QuestionID = new SelectList(db.QuestionSurveys.Where(q => q.ID == QuestionID), "ID", "Question");
            }

            else ViewBag.QuestionID = new SelectList(db.QuestionSurveys.Where(q => q.QuestionType.QuestionTypeGroupID == 1), "ID", "Question");
            ViewBag.returnUrl = returnURL;
            return View();
        }

        // POST: Management/SubQuestions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,QuestionID,Question")] SubQuestion subQuestion, string returnURL)
        {
            if (ModelState.IsValid)
            {
                db.SubQuestions.Add(subQuestion);
                await db.SaveChangesAsync();
                if (string.IsNullOrEmpty(returnURL))
                    return RedirectToAction("Index");
                else
                    return Redirect(returnURL);
            }

            ViewBag.QuestionID = new SelectList(db.QuestionSurveys, "ID", "Question", subQuestion.QuestionID);
            return View(subQuestion);
        }

        // GET: Management/SubQuestions/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubQuestion subQuestion = await db.SubQuestions.FindAsync(id);
            if (subQuestion == null)
            {
                return HttpNotFound();
            }
            ViewBag.QuestionID = new SelectList(db.QuestionSurveys, "ID", "Question", subQuestion.QuestionID);
            return View(subQuestion);
        }

        // POST: Management/SubQuestions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,QuestionID,Question")] SubQuestion subQuestion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subQuestion).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.QuestionID = new SelectList(db.QuestionSurveys, "ID", "Question", subQuestion.QuestionID);
            return View(subQuestion);
        }

        // GET: Management/SubQuestions/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubQuestion subQuestion = await db.SubQuestions.FindAsync(id);
            if (subQuestion == null)
            {
                return HttpNotFound();
            }
            return View(subQuestion);
        }

        // POST: Management/SubQuestions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            SubQuestion subQuestion = await db.SubQuestions.FindAsync(id);
            db.SubQuestions.Remove(subQuestion);
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
