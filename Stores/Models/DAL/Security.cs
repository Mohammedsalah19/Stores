﻿using System;
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
            if (HttpContext.Current.Session["userName"] != null)
            {
                string session = HttpContext.Current.Session["userID"].ToString();
                bool data = _db.Users_Privileges.Where(p => p.User_ID.ToString() == session).Select(f => f.users).FirstOrDefault();

                if (data == true)
                {
                    return true;
                }
            }
            return false;
        }

        public bool purchasebill()
        {
            if (HttpContext.Current.Session["userName"] != null)
            {
                string session = HttpContext.Current.Session["userID"].ToString();
                bool data = _db.Users_Privileges.Where(p => p.User_ID.ToString() == session).Select(f => f.purchasebill).FirstOrDefault();

                if (data == true)
                {
                    return true;
                }
            }
            return false;
        }

        public bool backbill()
        {
            if (HttpContext.Current.Session["userName"] != null)
            {
                string session = HttpContext.Current.Session["userID"].ToString();
                bool data = _db.Users_Privileges.Where(p => p.User_ID.ToString() == session).Select(f => f.backbill).FirstOrDefault();

                if (data == true)
                {
                    return true;
                }
            }
            return false;
        }
    }
}