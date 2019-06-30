using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stores.Models.CommonClasses
{
    public class EndDay
    {
        public IEnumerable<Bills>BillsX { get; set; }
        public IEnumerable<Payments>PaymentsX { get; set; }
        public IEnumerable<Expenses> ExpensesX { get; set; }
        public IEnumerable<ExpensesType> ExpensesTypeX { get; set; }
        public IEnumerable<Clients> ClientX { get; set; }
        public Users UserY { get; set; }

    }
}