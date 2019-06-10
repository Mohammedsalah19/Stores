using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stores.Models.CommonClasses
{
    public class ClientWithExten
    {

        public IEnumerable<Clients> ClientsX { get; set; }
        public IEnumerable<Clients_Type> Clients_Type { get; set; }
    }
}