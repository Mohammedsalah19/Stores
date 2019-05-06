using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Stores.Models
{
    public class ExpensesType
    {
        [Key]
        public int ExpensesType_ID { get; set; }
        [DisplayName("الاسم")]

        public string name { get; set; }
        [DisplayName("الوصف")]

        public string comment { get; set; }
    }
}