using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SurveyOnline.Models
{
    public class Department
    {
        [Key]
        public int ID { set; get; }
        [Display(Name ="Department Name")]
        public string DeptName { set; get; }
        public int WorkplaceID { set; get; }
        [ForeignKey("WorkplaceID")]
        public virtual Workplace Workplace { set; get; }
    }
}