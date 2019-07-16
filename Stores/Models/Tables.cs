using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Stores.Models
{
    public class Tables
    {
        [Key]
        public int ID { get; set; }

        public string name { get; set; }
        public string values { get; set; }
        //public string Bills { get; set; }
        //public string BillsCategory { get; set; }
        //public string BillsContent { get; set; }
        //public string Clients { get; set; }
        //public string Clients_Type { get; set; }
        //public string Expenses { get; set; }
        //public string ExpensesType { get; set; }
        //public string Payments { get; set; }
        //public string PlaceInfo { get; set; }
        //public string Products { get; set; }
        //public string ProductCategory { get; set; }
        //public string Product_Price { get; set; }
        //public string Storehouse { get; set; }
        //public string Users { get; set; }
        //public string Users_Privileges { get; set; }
    }
}