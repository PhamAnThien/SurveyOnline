using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SurveyOnline.Models
{
    public class QuestionSurvey
    {
        [Key]
        public int ID { set; get; }
        [DisplayName("Sorter")]
        public int? Sorter { set; get; }
        public string Question { set; get; }
        [DisplayName("Survey subject")]
        public int? SurveySubjectID { set; get; }
        [Display(Name = "Survey Group")]
        public int? QuestionTypeGroupID { set; get; }
        public int QuestionTypeID { set; get; }
        public int? SurveyGroupID { set; get; }
        [Display(Name ="Condition question",AutoGenerateField = true)]
        public bool isConditionQuestion { set; get; }
        public int? ConditionQuestionID { set; get; }
        public string ConditionValue { set; get; }
        public string ListSource { set; get; }

        public string Description { set; get; }
        [ForeignKey("SurveySubjectID")]
        public virtual SurveySubject SurveySubject { set; get; }

        [ForeignKey("QuestionTypeID")]
        public virtual QuestionType QuestionType { set; get; }
        [ForeignKey("SurveyGroupID")]
        public virtual SurveyGroup SurveyGroup { set; get; }
        [ForeignKey("QuestionTypeGroupID")]
        public virtual QuestionTypesGroup  QuestionTypesGroup { set; get; }

        public virtual ICollection<SubQuestion> SubQuestions { set; get; }
        public virtual ICollection<ListTable> ListAnswer { set; get; }
    }
}