using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Stores.Models.DAL
{
    public class PrintType
    {
        [Key]
        public int ID { get; set; }


        public string PrinterName { get; set; }

        public string Description { get; set; }
    }
}