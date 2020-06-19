using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SurveyOnline.Models
{
    public class AnswerCollection
    {
        [Key]
        public int ID { set; get; }
        public string AnswerID { set; get; }
        public string UserID { set; get; }
        public int SurveySubjectId { set; get; }
        public int SurveyGroupID { set; get; }
        public int SurveyQuestionID { set; get; }
        public int SurveyQuestionTypeID { set; get; }

        public string SurveyAnswer { set; get; }
        public DateTime CreateDate { set; get; }
    }
}