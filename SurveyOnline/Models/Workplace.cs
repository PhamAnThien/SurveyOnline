using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SurveyOnline.Models
{
    public class Workplace
    {
        [Key]
        public int ID { set; get; }
        [Display(Name ="Name")]
        public string Name { set; get; }

        [Display(Name="Type of Workplace")]
        public int WorkplaceTypeID { set; get; }
        [Display(Name ="Address")]
        public string Address { set; get; }
        [Phone]
        [Display(Name ="Phone number")]
        public string PhoneNumber { set; get; }
        [EmailAddress]
        [Display(Name="Email contact")]
        public string Email { set; get; }
        [Display(Name="Tax code")]
        public string TaxCode { set; get; }
        [Display(Name ="Name of Manager")]
        public string ManagerName { set; get; }
        [ForeignKey("WorkplaceTypeID")]
        public virtual WorkplaceType WorkplaceType { set; get; }

    }
}