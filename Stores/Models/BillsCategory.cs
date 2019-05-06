using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Stores.Models
{
    public class BillsCategory
    {
        [Key]
        public int BillCate_ID { get; set; }
        [DisplayName("اسم الفاتوره")]

        public string name { get; set; }
        [DisplayName("الوصف")]

        public string description { get; set; }
    }
}