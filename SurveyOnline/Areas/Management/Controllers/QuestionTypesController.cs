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
    public class QuestionTypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Management/QuestionTypes
        public async Task<ActionResult> Index()
        {
            return View(await db.QuestionTypes.ToListAsync());
        }

        // GET: Management/QuestionTypes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionType questionType = await db.QuestionTypes.FindAsync(id);
            if (questionType == null)
            {
                return HttpNotFound();
            }
            return View(questionType);
        }

        // GET: Management/QuestionTypes/Create
        //public ActionResult Create()
        //{
        //    ViewBag.QuestionTypeGroupID = new SelectList(db.QuestionTypesGroups, "ID", "Name");
        //    return View();
        //}
        public ActionResult Create(int? QuestionTypeGroupID)
        {
            if (QuestionTypeGroupID != null)
                ViewBag.QuestionTypeGroupID = new SelectList(db.QuestionTypesGroups.Where(s => s.ID == QuestionTypeGroupID), "ID", "Name");
            else
                ViewBag.QuestionTypeGroupID = new SelectList(db.QuestionTypesGroups, "ID", "Name");
            return View();
        }

        // POST: Management/QuestionTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,QuestionTypeGroupID,QuestionTypeName")] QuestionType questionType)
        {
            if (ModelState.IsValid)
            {
                db.QuestionTypes.Add(questionType);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(questionType);
        }

        // GET: Management/QuestionTypes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionType questionType = await db.QuestionTypes.FindAsync(id);
            if (questionType == null)
            {
                return HttpNotFound();
            }
            ViewBag.QuestionTypeGroupID = new SelectList(db.QuestionTypesGroups, "ID", "Name");
            return View(questionType);
        }

        // POST: Management/QuestionTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,QuestionTypeName,QuestionTypeGroupID")] QuestionType questionType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(questionType).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(questionType);
        }

        // GET: Management/QuestionTypes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionType questionType = await db.QuestionTypes.FindAsync(id);
            if (questionType == null)
            {
                return HttpNotFound();
            }
            return View(questionType);
        }

        // POST: Management/QuestionTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            QuestionType questionType = await db.QuestionTypes.FindAsync(id);
            db.QuestionTypes.Remove(questionType);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<ActionResult> getListQuestionType(int? GroupTypeQuestionID)
        {
            if (GroupTypeQuestionID == null)
            {
                var ListQuestionType = new SelectList(await db.QuestionTypes.ToListAsync(), "ID", "QuestionTypeName");
                return Json(ListQuestionType, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var ListQuestionType = new SelectList(await db.QuestionTypes.Where(q=>q.QuestionTypeGroupID == GroupTypeQuestionID).ToListAsync(), "ID", "QuestionTypeName");
                return Json(ListQuestionType, JsonRequestBehavior.AllowGet);
            }
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
