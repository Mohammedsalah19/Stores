using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stores.Models.DAL
{
    public class Security
    {
        ProjectContext _db = new ProjectContext();

        public bool Users()
        {
            string session = HttpContext.Current.Session["userID"].ToString();
            bool data = _db.Users_Privileges.Where(p => p.User_ID.ToString() == session).Select(f => f.users).FirstOrDefault();

            if (data == true)
            {
                return true;

            }
            return false;
        }

    }
}