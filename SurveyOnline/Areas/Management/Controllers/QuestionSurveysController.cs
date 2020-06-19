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
    public class QuestionSurveysController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Management/QuestionSurveys
        public async Task<ActionResult> Index()
        {
            var questionSurveys = db.QuestionSurveys.Include(q => q.QuestionType).Include(q => q.QuestionTypesGroup).Include(q => q.SurveyGroup).Include(q => q.SurveySubject);
            return View(await questionSurveys.ToListAsync());
        }

        // GET: Management/QuestionSurveys/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionSurvey questionSurvey = await db.QuestionSurveys.FindAsync(id);
            if (questionSurvey == null)
            {
                return HttpNotFound();
            }
            return View(questionSurvey);
        }

        // GET: Management/QuestionSurveys/Create
        public ActionResult Create(int? QuestionTypeID, int? QuestionTypeGroupID, int? SurveySubjectID, int? SurveyGroupID, string returnUrl)
        {

            if (QuestionTypeID != null)
            {
                var QuestionType = db.QuestionTypes.Find(QuestionTypeID);
                ViewBag.QuestionTypeGroupID = new SelectList(db.QuestionTypesGroups.Where(qg => qg.ID == QuestionType.QuestionTypeGroupID), "ID", "Name");
                ViewBag.QuestionTypeID = new SelectList(db.QuestionTypes.Where(q => q.QuestionTypeGroupID == QuestionTypeID), "ID", "QuestionTypeName");
            }
            else
            {
                if (QuestionTypeGroupID != null)
                {
                    ViewBag.QuestionTypeGroupID = new SelectList(db.QuestionTypesGroups.Where(qg => qg.ID == QuestionTypeGroupID), "ID", "Name");
                    ViewBag.QuestionTypeID = new SelectList(db.QuestionTypes.Where(q => q.QuestionTypeGroupID == QuestionTypeGroupID), "ID", "QuestionTypeName");
                }
                else
                {
                    ViewBag.QuestionTypeID = new SelectList(db.QuestionTypes, "ID", "QuestionTypeName", "QuestionTypeGroupID");
                    ViewBag.QuestionTypeGroupID = new SelectList(db.QuestionTypesGroups, "ID", "Name");
                }
            }
            if (SurveyGroupID != null)
            {
                var surveyGroup = db.SurveyGroups.Find(SurveyGroupID);
                ViewBag.SurveyGroupID = new SelectList(db.SurveyGroups.Where(sg => sg.ID == SurveyGroupID), "ID", "Name");
                ViewBag.SurveySubjectID = new SelectList(db.SurveySubjects.Where(s => s.ID == surveyGroup.SurveySubjectId), "ID", "SubjectName");
                ViewBag.ConditionQuestionID = new SelectList(db.QuestionSurveys.Where(q => q.SurveyGroupID == SurveyGroupID), "ID", "Question");
            }
            else
            {
                if (SurveySubjectID != null)
                {
                    ViewBag.SurveyGroupID = new SelectList(db.SurveyGroups.Where(sg => sg.SurveySubjectId == SurveySubjectID), "ID", "Name");
                    ViewBag.SurveySubjectID = new SelectList(db.SurveySubjects.Where(s => s.ID == SurveySubjectID), "ID", "SubjectName");
                    ViewBag.ConditionQuestionID = new SelectList(db.QuestionSurveys.Where(q => q.SurveySubjectID == SurveySubjectID), "ID", "Question");
                }
                else
                {
                    ViewBag.SurveyGroupID = new SelectList(db.SurveyGroups, "ID", "Name");
                    ViewBag.SurveySubjectID = new SelectList(db.SurveySubjects, "ID", "SubjectName");
                    ViewBag.ConditionQuestionID = new SelectList(db.QuestionSurveys, "ID", "Question");
                }
            }
            ViewBag.ListSource = new SelectList(db.ListTables.Where(l => l.ParentID == null), "Name", "Name");
            ViewBag.ConditionValue = new SelectList(new List<string>());
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        // POST: Management/QuestionSurveys/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Sorter,Question,SurveySubjectID,QuestionTypeGroupID,QuestionTypeID,SurveyGroupID,isConditionQuestion,ConditionQuestionID,ConditionValue,ListSource,Description")] QuestionSurvey questionSurvey, string returnURL)
        {
            if (ModelState.IsValid)
            {
                db.QuestionSurveys.Add(questionSurvey);
                await db.SaveChangesAsync();
                if (!string.IsNullOrEmpty(returnURL))
                {
                    return Redirect(returnURL);
                }
                return RedirectToAction("Index");
            }

            ViewBag.QuestionTypeID = new SelectList(db.QuestionTypes, "ID", "QuestionTypeName", questionSurvey.QuestionTypeID);
            ViewBag.QuestionTypeGroupID = new SelectList(db.QuestionTypesGroups, "ID", "Name", questionSurvey.QuestionTypeGroupID);
            ViewBag.SurveyGroupID = new SelectList(db.SurveyGroups, "ID", "Name", questionSurvey.SurveyGroupID);
            ViewBag.SurveySubjectID = new SelectList(db.SurveySubjects, "ID", "SubjectName", questionSurvey.SurveySubjectID);
            return View(questionSurvey);
        }

        // GET: Management/QuestionSurveys/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionSurvey questionSurvey = await db.QuestionSurveys.FindAsync(id);
            if (questionSurvey == null)
            {
                return HttpNotFound();
            }
            ViewBag.QuestionTypeID = new SelectList(db.QuestionTypes, "ID", "QuestionTypeName", questionSurvey.QuestionTypeID);
            ViewBag.QuestionTypeGroupID = new SelectList(db.QuestionTypesGroups, "ID", "Name", questionSurvey.QuestionTypeGroupID);
            ViewBag.SurveyGroupID = new SelectList(db.SurveyGroups, "ID", "Name", questionSurvey.SurveyGroupID);
            ViewBag.SurveySubjectID = new SelectList(db.SurveySubjects, "ID", "SubjectName", questionSurvey.SurveySubjectID);
            ViewBag.ListSource = new SelectList(db.ListTables.Where(l => l.ParentID == null), "Name", "Name");
            return View(questionSurvey);
        }

        // POST: Management/QuestionSurveys/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Sorter,Question,SurveySubjectID,QuestionTypeGroupID,QuestionTypeID,SurveyGroupID,isConditionQuestion,ConditionQuestionID,ConditionValue,ListSource,Description")] QuestionSurvey questionSurvey)
        {
            if (ModelState.IsValid)
            {
                db.Entry(questionSurvey).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.QuestionTypeID = new SelectList(db.QuestionTypes, "ID", "QuestionTypeName", questionSurvey.QuestionTypeID);
            ViewBag.QuestionTypeGroupID = new SelectList(db.QuestionTypesGroups, "ID", "Name", questionSurvey.QuestionTypeGroupID);
            ViewBag.SurveyGroupID = new SelectList(db.SurveyGroups, "ID", "Name", questionSurvey.SurveyGroupID);
            ViewBag.SurveySubjectID = new SelectList(db.SurveySubjects, "ID", "SubjectName", questionSurvey.SurveySubjectID);
            return View(questionSurvey);
        }

        // GET: Management/QuestionSurveys/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionSurvey questionSurvey = await db.QuestionSurveys.FindAsync(id);
            if (questionSurvey == null)
            {
                return HttpNotFound();
            }
            return View(questionSurvey);
        }

        // POST: Management/QuestionSurveys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            QuestionSurvey questionSurvey = await db.QuestionSurveys.FindAsync(id);
            db.QuestionSurveys.Remove(questionSurvey);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<ActionResult> GetListQuestion(int? SurveySubjectID, int? SurveyGroupID)
        {
            SelectList QuestionID;
            if (SurveySubjectID == null && SurveyGroupID == null)
            {
                QuestionID = new SelectList(await db.QuestionSurveys.ToListAsync(), "ID", "Question");
            }
            else if (SurveySubjectID != null && SurveyGroupID == null)
            {
                QuestionID = new SelectList(await db.QuestionSurveys.Where(q =>  q.SurveySubjectID == SurveySubjectID).ToListAsync(), "ID", "Question");
            }
            else if (SurveySubjectID == null && SurveyGroupID != null)
            {
                QuestionID = new SelectList(await db.QuestionSurveys.Where(q => q.SurveyGroupID == SurveyGroupID ).ToListAsync(), "ID", "Question");
            }
            else
             QuestionID = new SelectList(await db.QuestionSurveys.Where(q => q.SurveyGroupID == SurveyGroupID && q.SurveySubjectID == SurveySubjectID).ToListAsync(), "ID", "Question");

            return Json(QuestionID, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> getListSourceOfQuestion(int? QuestionID)
        {
            SelectList ConditionValue;
            if (QuestionID == null )
                ConditionValue = new SelectList(new List<string>());
            else
            {
                var question = db.QuestionSurveys.Find(QuestionID);
                var answer = db.ListTables.FirstOrDefault(l => l.Name == question.ListSource);
                ConditionValue = new SelectList(db.ListTables.Where(l=>l.ParentID == answer.ID),"ID","Name");
            }
            return Json(ConditionValue, JsonRequestBehavior.AllowGet);
        }
        public ActionResult getConditionQuestion(int SurveySubjectID)
        {
            var question = db.QuestionSurveys.Where(q => q.SurveySubjectID == SurveySubjectID).ToList();
            var condition = new SelectList(question, "ID", "ConditionQuestionID", "ConditionValue");
            return Json(condition, JsonRequestBehavior.AllowGet);
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
