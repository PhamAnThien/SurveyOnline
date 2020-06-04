using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SurveyOnline.Models
{
    public class Course
    {
        [Key]
        public int ID { set; get; }
        [Display(Name = "Course name")]
        public string Name { set; get; }

        public int WorkplaceID { set; get; }
        [ForeignKey("WorkplaceID")]
        public virtual Workplace Workplace { set; get; }
    }
}