using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurveyOnline.Models
{
    public class SurveyAnalysis
    {
        public string subjectSurvey { set; get; }
        public int CountAnswer { set; get; }
        public List<AnswerAnalysis> answerAnalyses { set; get; }
    }
    public class AnswerAnalysis
    {
        public string Question { set; get; }
        public List<AnswerAggregate> answerAggregates { set; get; }
    }
    public class AnswerAggregate
    {
        public string Question { set; get; }
        public int Count { set; get; }
        public List<SubQuestionAggregate> subQuestionAggregates { set; get; }
    }
    public class SubQuestionAggregate
    {
        public string SubQuestionName { set; get; }
        public string answer { set; get; }
        public List<string> Selection { set; get; }
        public List<int> Counts { set; get; }
        public List<SubAnswer> subAnswers { set; get; }
    }

    public class SubAnswer
    {
        public int ID { set; get; }
        public string Answer { set; get; }
        public int count { set; get; }
    }
}