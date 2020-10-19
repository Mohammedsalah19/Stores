using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Stores.Models
{
    public class Clients
    {
        [Key]
        public int Client_ID { get; set; }
        [DisplayName("الاسم")]

        public string name { get; set; }
        [DisplayName("الهاتف")]

        public string phone { get; set; }
        [DisplayName("الرقم القومى")]

        public string nationalID { get; set; }
        [DisplayName("العنوان")]

        public string Address { get; set; }
        [DisplayName("ملاحظة")]

        public string Comment { get; set; }
        [DisplayName("الحاله")]

        public bool Active { get; set; }
        [DisplayName("اقل عدد فواتير")]

        public decimal minimum_bills { get; set; }
        [DisplayName("النوع")]

        public virtual int Clients_Type_ID { get; set; }
    }
}