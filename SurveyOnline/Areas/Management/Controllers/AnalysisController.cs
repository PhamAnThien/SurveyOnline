using SurveyOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SurveyOnline.Areas.Management.Controllers
{
    [Authorize]
    public class AnalysisController : Controller
    {
        // GET: Management/Analysis
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var survey = db.SurveySubjects.ToList();
            return View(survey);
        }
        public async Task<ActionResult> Analiysis(int? ID)
        {
            SurveyAnalysis surveyAnalysis = new SurveyAnalysis();
            List<AnswerAnalysis> AnswerAnalysises = new List<AnswerAnalysis>();
            if (ID == null) return HttpNotFound();
            var answer = db.AnswerCollections.Where(a => a.SurveySubjectId == ID).ToList();
            var subject = db.SurveySubjects.Find(ID);
            surveyAnalysis.subjectSurvey = subject.SubjectName;
            surveyAnalysis.CountAnswer = answer.Select(m => m.AnswerID).Distinct().Count();
            var questions = db.QuestionSurveys.Where(q => q.SurveySubjectID == ID).ToList();
            foreach (var question in questions)
            {
                AnswerAnalysis answerAnalysis = new AnswerAnalysis();
                answerAnalysis.Question = question.Question;
                answerAnalysis.answerAggregates = new List<AnswerAggregate>();
                var ans = answer.Where(a => a.SurveyQuestionID == question.ID).OrderBy(a => a.SurveyAnswer).ToList().Select(a => a.SurveyAnswer).ToList();
                var subQuestion = db.SubQuestions.Where(s => s.QuestionID == question.ID).Select(s => s.Question).ToList();
                var ListTable = db.ListTables.FirstOrDefault(l => l.Name == question.ListSource);

                List<SubQuestionAggregate> subQuestionAggregates = new List<SubQuestionAggregate>();
                foreach (var sub in subQuestion)
                {
                    List<SubAnswer> subAnswers = new List<SubAnswer>();
                    if (ListTable != null)
                    {
                        var ListTables = db.ListTables.Where(l => l.ParentID == ListTable.ID).ToList();
                        foreach (var choice in ListTables)
                        {
                            subAnswers.Add(new SubAnswer() { ID = choice.ID, Answer = choice.Name, count = 0 });
                        }
                    }
                    SubQuestionAggregate subQuestionAggregate = new SubQuestionAggregate() { SubQuestionName = sub, subAnswers = subAnswers };
                    subQuestionAggregates.Add(subQuestionAggregate);
                }
                var dictint = ans.Distinct().ToList();
                if (question.QuestionTypeID < 34)
                {
                    foreach (var dict in ans)
                    {
                        var answerSubQuestion = dict.Split(';').ToList();
                        while (subQuestion.Count > answerSubQuestion.Count)
                        {
                            answerSubQuestion.Add("");
                        }
                        for (int i = 0; i < subQuestionAggregates.Count; i++) //get subQuestionAggregates[i] and answerSubQuestion[i]
                        {
                            string SubAnswerTest = answerSubQuestion.ElementAt(i);
                            var subAnswertest = subQuestionAggregates.ElementAt(i);
                            //subAnswertest.subAnswers
                            for (int j = 0; j < subAnswertest.subAnswers.Count; j++)
                            {
                                string AnswerSubQuest = subQuestionAggregates.ElementAt(i).SubQuestionName + " / " + subQuestionAggregates.ElementAt(i).subAnswers.ElementAt(j).ID + ":" + subQuestionAggregates.ElementAt(i).subAnswers.ElementAt(j).Answer;
                                int AnswerSubQuestCount = subQuestionAggregates.ElementAt(i).subAnswers.ElementAt(j).count;
                                if (!string.IsNullOrEmpty(SubAnswerTest) && subQuestionAggregates.ElementAt(i).subAnswers.ElementAt(j).ID == int.Parse(SubAnswerTest))
                                {
                                    subQuestionAggregates.ElementAt(i).subAnswers.ElementAt(j).count++;
                                    break;
                                }
                            }
                        }
                    }

                    AnswerAggregate answerAggregate = new AnswerAggregate() { Question = question.Question, Count = 0, subQuestionAggregates = subQuestionAggregates };
                    answerAnalysis.answerAggregates.Add(answerAggregate);
                }
                else
                    foreach (var dict in dictint)
                    {
                        string Question = dict;
                        switch (question.QuestionTypeID)
                        {
                            case 38:
                                break;
                            case 40:
                                if (dict.Contains("1"))
                                    Question = "Nam";
                                else Question = "Nữ";
                                break;
                            case 42:
                                break;
                            case 43:
                                break;
                            case 48:
                                Question = db.ListTables.Find(int.Parse(dict)).Name;
                                break;
                            case 49:
                                Question = db.ListTables.Find(int.Parse(dict)).Name;
                                break;
                            case 50:
                                break;
                            case 51:
                                break;

                        }
                        var a = ans.Where(b => b.Contains(dict)).ToList().Count;
                        AnswerAggregate answerAggregate = new AnswerAggregate() { Question = Question, Count = a };
                        answerAnalysis.answerAggregates.Add(answerAggregate);
                    }
                AnswerAnalysises.Add(answerAnalysis);
            }
            surveyAnalysis.answerAnalyses = AnswerAnalysises;
            return View(surveyAnalysis);
        }
    }
}