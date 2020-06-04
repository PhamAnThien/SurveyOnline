using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SurveyOnline.Models
{
    public class SurveyGroup
    {
        [Key]
        public int ID { set; get; }
        [Display(Name ="Group Name")]
        public string Name { set; get; }

        [Display(Name ="Survey subject")]
        public int SurveySubjectId { set; get; }

        [ForeignKey("SurveySubjectId")]
        public virtual SurveySubject SurveySubject { set; get; }
        public virtual ICollection<QuestionSurvey> QuestionSurveys { set; get; }
    }
}