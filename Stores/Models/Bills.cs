using Stores.Models.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Stores.Models
{
    public class Bills
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("السعر")]

        public decimal price { get; set; }
        [DisplayName("التكلفه")]

        public decimal cost { get; set; }
        [DisplayName("التاريخ")]

        public DateTime date { get; set; }
        [DisplayName("الخصم")]

        public decimal discount { get; set; }
        [DisplayName("الحاله")]

        public string status { get; set; }
        [DisplayName("الوصف")]

        public string Comment { get; set; }
        public virtual Users User_ID { get; set; }
        public virtual Clients Client_ID { get; set; }

        public virtual BillsCategory Cate_Id { get; set; }


    }
}