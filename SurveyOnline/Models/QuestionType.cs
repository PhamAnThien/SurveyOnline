using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SurveyOnline.Models
{
    public class QuestionType
    {
        [Key]
        public int ID { set; get; }
        [Display(Name ="Group of type question")]
        public int QuestionTypeGroupID { set; get; }
        [Display(Name ="Name of Question type")]
        public string QuestionTypeName { set; get; }

        [ForeignKey("QuestionTypeGroupID")]
        public virtual QuestionTypesGroup QuestionTypesGroup { set; get; }
    }
}