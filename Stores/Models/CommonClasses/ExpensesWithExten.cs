using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stores.Models.CommonClasses
{
    public class ExpensesWithExten
    {

        public IEnumerable<Expenses> ExpensesX { get; set; }
        public IEnumerable<ExpensesType> ExpensesTypeX { get; set; }
    }
}