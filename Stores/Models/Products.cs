using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Stores.Models
{
    public class Products
    {
        [Key]
        public int Pro_id { get; set; }
        [DisplayName("اسم المنتج")]

        public string name { get; set; }
        [DisplayName("الباركود")]

        public string barcode { get; set; }
        [DisplayName("حاله المنتج")]

        public bool active { get; set; }

        public virtual  ProductCategory Cate_ID { get; set; }

    }
}