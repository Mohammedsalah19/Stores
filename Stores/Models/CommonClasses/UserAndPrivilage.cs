using Stores.Models.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Stores.Models.CommonClasses
{
    public class UserAndPrivilage
    {
        [Key]
        public int id { get; set; }
        public IEnumerable<Users> UserX { get; set; }
        public IEnumerable<Users_Privileges> PreviX { get; set; }
        public IEnumerable<PrintType> PrintTypeX { get; set; }
    }
}