using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SurveyOnline.Models
{
    public class QuestionTypesGroup
    {
        [Key]
        public int ID { set; get; }
        [Display(Name ="Group type")]
        public string Name { set; get; }

        public virtual ICollection<QuestionType> QuestionTypes { set; get; }
    }
}