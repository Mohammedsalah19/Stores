using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Stores.Models
{
    public class Produt_Price
    {
        [Key]
        public int Prd_Pri_ID { get; set; }

        [DisplayName("التكلفه")]

        public decimal cost { get; set; }
        [DisplayName("سعر البيع")]

        public decimal Price { get; set; }
        [DisplayName("سعر الحمله")]

        public decimal many_price { get; set; }
        [DisplayName("الكميه")]

        public int Quantity { get; set; }
        [DisplayName("اقل كميه فى المخزن")]


        public int Minmum { get; set; }

        [DisplayName("الخصم او العرض")]

        public decimal Discount { get; set; }

        public virtual Products Pro_ID { get; set; }
        public virtual Storehouse Store_Id { get; set; }

    }
}