using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Stores.Models
{
    public class Expenses
    {
        [Key]
        public int Expenses_ID { get; set; }
        [DisplayName("الكميه")]

        public decimal amount { get; set; }
        [DisplayName("التاريخ")]

        public DateTime date { get; set; }
        [DisplayName("الوصف")]

        public string comment { get; set; }

        public virtual ExpensesType ExpensesType_ID{ get; set; }
        public virtual Users User_ID{ get; set; }


    }
}