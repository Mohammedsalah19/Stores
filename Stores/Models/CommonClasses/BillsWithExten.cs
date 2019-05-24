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
        public Products productY { get; set; }


        public IEnumerable<ProductCategory> prodCategoryX { get; set; }
        public ProductCategory prodCategoryY { get; set; }


        public IEnumerable<Bills> billsX { get; set; }
        public Bills billsY { get; set; }

        public IEnumerable<BillsContent> billCOntentX { get; set; }
        public BillsContent billContentY { get; set; }

        public IEnumerable<Clients> ClientsX{ get; set; }
        public Clients ClientsY{ get; set; }

        public IEnumerable<Storehouse> StorehouseX { get; set; }


    }
}