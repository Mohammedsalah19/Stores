using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stores.Models.CommonClasses
{
    public class NotificationWithExten
    {

        public IEnumerable<Produt_Price> ProPricX { get; set; }
        public IEnumerable<Products> ProductX { get; set; }
        public IEnumerable<Storehouse> StorehousesX { get; set; }
    }
}