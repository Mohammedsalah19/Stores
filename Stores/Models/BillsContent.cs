using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Stores.Models
{
    public class BillsContent
    {
        [Key]
        public int BillsContent_ID { get; set; }
        [DisplayName("الكميه")]

        public decimal Quantity { get; set; }
        [DisplayName("السعر")]

        public decimal Price { get; set; }
        [DisplayName("التكلفه")]

        public decimal Cost { get; set; }
        [DisplayName("الحاله")]

        public bool IsDeleted { get; set; }
        [DisplayName("الوصف")]

        public string Comment { get; set; }
        public virtual int Bill_ID { get; set; }
        public virtual int Product_ID { get; set; }

        public bool Viewed { get; set; }

    }
}