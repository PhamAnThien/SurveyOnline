using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SurveyOnline.Models
{
    public class WorkplaceType
    {
        [Key]
        public int ID { set; get; }
        [Display(Name = "Type of Workplace")]
        public string Name { set; get; }

        
        public virtual ICollection<Workplace> Workplaces { set; get; }
    }
}