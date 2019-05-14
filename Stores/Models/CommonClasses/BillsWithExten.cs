using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Stores.Models.CommonClasses
{
    public class BillsWithExten
    {

        [Key]
        public int id { get; set; }
        public IEnumerable<Products> productX { get; set; }
        public IEnumerable<ProductCategory> prodCategoryX { get; set; }
        public IEnumerable<Bills> billsX { get; set; }


    }
}