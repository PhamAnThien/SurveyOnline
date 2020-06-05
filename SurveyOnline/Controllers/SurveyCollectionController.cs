﻿using SurveyOnline.Models;
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
        public  ActionResult  Index()
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
        public async Task<ActionResult> getSurveyAnswer(object value)
        {

            return RedirectToAction("Index");
        }
    }
}