using Stores.Models;
using Stores.Models.DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stores.Controllers
{
    public class EmployeeController : Controller
    {
        private ProjectContext _db = new ProjectContext();
        private   Security s = new Security();

        // GET: Employee
        public ActionResult Index()
        {


            return View(_db.Users.ToList());
        }
        [HttpGet]
        public ActionResult NewEmployee()
        {
          bool res=  s.Users();
            if (res == true)
            {
                return View();

            }
            return RedirectToAction("Index");

        }
        [HttpPost]
        public ActionResult NewEmployee(Users _user,Users_Privileges _userprevli ,string pic)
        {
            if (ModelState.IsValid)
            {

                string filName = Path.GetFileNameWithoutExtension(_user.Pic.FileName);

                string exten = Path.GetExtension(_user.Pic.FileName);
                filName = filName + DateTime.Now.ToString("yymmssff") + exten;
                _user.PicPath = @"\images\" + filName;


                filName = Path.Combine(Server.MapPath(@"~\images\"), filName);

                _user.Pic.SaveAs(filName);

                _user.user_current_date = DateTime.Now;
            _db.Users.Add(_user);
            _db.SaveChanges();

            // add check boxs
            _userprevli.User_ID = _user.Id;
            _db.Users_Privileges.Add(_userprevli);

            _db.SaveChanges();


            return RedirectToAction("Index");
            }

            return View();
        }


        [HttpGet]
        public ActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Login(Users _users)
        {

            var model = _db.Users.Where(p => p.username == _users.username && p.Password == _users.Password).FirstOrDefault();
            if (model !=null)
            {
                Session["userName"] = model.username;
                Session["userID"] = model.Id;

                return RedirectToAction("Index","Home");
            }
            return View();
        }


        public ActionResult LogOff(Users _users)
        {
            if (Session["userName"] != null)
            {
                Session.Clear();


            }
            return RedirectToAction("Login");
        }
    }
}