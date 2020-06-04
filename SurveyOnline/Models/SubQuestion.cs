using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SurveyOnline.Models
{
    public class SubQuestion
    {
        [Key]
        public int ID { set; get; }
        public int QuestionID { set; get; }
        public string Question { set; get; }
        [ForeignKey("QuestionID")]
        public virtual QuestionSurvey QuestionSurvey { set; get; }
    }
}