using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Stores.Models
{
    public class Storehouse
    {
        [Key]
        public int Store_Id { get; set; }
        [DisplayName("اسم المخزن")]

        public string name { get; set; }
        [DisplayName("المكان")]

        public string place { get; set; }
        [DisplayName("الوصف")]

        public string Description { get; set; }
    }
}