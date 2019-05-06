using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Stores.Models
{
    public class Clients_Type
    {
        [Key]
        public int Clients_Type_id { get; set; }

        [DisplayName("نوع العميل")]

        public string name { get; set; }
        [DisplayName("  الوصف")]

        public string Description { get; set; }
    }
}