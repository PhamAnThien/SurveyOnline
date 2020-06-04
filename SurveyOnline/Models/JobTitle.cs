﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SurveyOnline.Models
{
    public class JobTitle
    {
        [Key]
        public int ID { set; get; }
        [Display(Name="Name of Job")]
        public string Name { set; get; }
    }
}