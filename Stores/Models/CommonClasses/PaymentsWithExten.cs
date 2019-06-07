using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stores.Models.CommonClasses
{
    public class PaymentsWithExten
    {

         public IEnumerable<Clients> ClientsX { get; set; }
         public Clients ClientsY { get; set; }

        public IEnumerable<Users> UsersX { get; set; }

        public IEnumerable<Payments> PaymentX { get; set; }
        public Payments PaymentY { get; set; }

        public IEnumerable<Clients_Type> Clients_TypeX { get; set; }

    }
}