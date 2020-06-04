using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SurveyOnline.Models
{
    public class ListTable
    {
        [Key]
        public int ID { set; get; }

        public string Name { set; get; }

        public int? ParentID { set; get; }

        [ForeignKey("ParentID")]
        public virtual ListTable ParentListTable { set; get; }

        public virtual ICollection<ListTable> ListTables { set; get; }
    }
}