using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SurveyOnline.Models
{
    public class AnswerTemplates
    {
        [Key]
        public int ID { set; get; }
        [Display(Name ="Question Type")]
        public int QuestTypeID { set; get; }
        [Display(Name ="Answer")]
        public int AnswerUnitID { set; get; }
        public int Value { set; get; }

        [ForeignKey("QuestTypeID")]
        public virtual QuestionType QuestionType { set; get; }
        [ForeignKey("AnswerUnitID")]
        public virtual AnswerUnit AnswerUnit { set; get; }
    }
}