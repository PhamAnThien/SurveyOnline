using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SurveyOnline.Models
{
    public class SurveyProfile
    {
        [Key]
        public int ID { set; get; }
        [Display(Name = "Fullname")]
        public string Name { set; get; }
        public DateTime Birthday { set; get; }
        public string Gender { set; get; }
        public string Address { set; get; }
        [Display(Name="Job title")]
        public int JobID { set; get; }
        [Display(Name ="Work place")]
        public int WorkplaceID { set; get; }

        [ForeignKey("JobID")]
        public virtual JobTitle JobTitle { set; get; }
        [ForeignKey("WorkplaceID")]
        public virtual Workplace Workplace { set; get; }
    }
}