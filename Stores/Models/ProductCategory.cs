using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Stores.Models
{
    public class ProductCategory
    {
        [Key]
        public int Cate_ID { get; set; }

        [DisplayName("اسم الصنف")]
        public string name { get; set; }
        [DisplayName("الوصف")]
        public string description { get; set; }
    }
}