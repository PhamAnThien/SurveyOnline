using SurveyOnline.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SurveyOnline.Controllers
{
    public class SurveyCollectionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: SurveyCollection
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Survey(int? IdSurvey)
        {
            if (IdSurvey == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var survey = await db.SurveySubjects.FindAsync(IdSurvey);
            if (survey == null)
            {
                return HttpNotFound();
            }
            var SurveyGroups = await db.SurveyGroups.Where(g => g.SurveySubjectId == survey.ID).ToListAsync();
            List<QuestionSurvey> questionSurveyLists = new List<QuestionSurvey>();
            foreach (var item in SurveyGroups)
            {
                var questionSurveys = await db.QuestionSurveys.Where(g => g.SurveyGroupID == item.ID).ToListAsync();
                item.QuestionSurveys = questionSurveys;
                foreach (var question in questionSurveys)
                {
                    switch (question.ListSource)
                    {
                        case "Branches":
                            ViewBag.Branches = new SelectList(await db.Branches.ToListAsync(), "ID", "Name");
                            break;
                        case "Course":
                            ViewBag.Course = new SelectList(await db.Courses.ToListAsync(), "ID", "Name");
                            break;
                        case "Department":
                            ViewBag.Department = new SelectList(await db.Departments.ToListAsync(), "ID", "Name");
                            break;
                    }
                    ViewBag.ListSource = new SelectList(await db.ListTables.Where(l => l.ParentListTable.Name == question.ListSource).ToListAsync(), "ID", "Name");

                    question.SubQuestions = await db.SubQuestions.Where(s => s.QuestionID == question.ID).ToListAsync();
                    question.ListAnswer = await db.ListTables.Where(l => l.ParentListTable.Name == question.ListSource).ToListAsync();
                }
            }

            survey.SurveyGroups = SurveyGroups;
            return View(survey);
        }
        [HttpPost]
        public async Task<ActionResult> getSurveyAnswer(string[][] data)
        {

            string answerID = Guid.NewGuid().ToString();
            try
            {
                foreach (var answer in data)
                {
                    var questionTypeID = db.QuestionSurveys.Find(int.Parse(answer[2])).QuestionTypeID;
                    AnswerCollection a = new AnswerCollection()
                    {
                        ID= 0,
                        AnswerID = answerID,
                        SurveySubjectId = int.Parse(answer[0]),
                        SurveyGroupID = int.Parse(answer[1]),
                        SurveyQuestionTypeID = questionTypeID,
                        SurveyQuestionID = int.Parse(answer[2]),
                        SurveyAnswer = answer[3]==null? "": answer[3],
                        UserID = HttpContext.User.Identity.Name,
                        CreateDate = DateTime.Now

                    };
                    db.AnswerCollections.Add(a);
                }
                db.SaveChanges();
            }
            catch (Exception e)
            {


            }
            return RedirectToAction("Index");
        }
    }
}