using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SurveyOnline.Models
{
    public class SurveySubject
    {
        [Key]
        public int ID { set; get; }

        [Display(Name ="Subject")]
        public string SubjectName { set; get; }
        [Display(Name ="Description")]
        public string Description { set; get; }
        [Display(Name = "Description footer")]
        public string DescriptionFooter { set; get; }

        public virtual ICollection<SurveyGroup> SurveyGroups { set; get; }
        public virtual ICollection<QuestionSurvey> QuestionSurveys { set; get; }
    }
}