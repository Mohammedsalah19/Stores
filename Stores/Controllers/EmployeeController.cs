using Stores.Models;
using Stores.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stores.Controllers
{
    public class EmployeeController : Controller
    {
        private ProjectContext _db = new ProjectContext();
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult NewEmployee()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NewEmployee(Users _user,Users_Privileges _userprevli)
        {

            _db.Users.Add(_user);
            _db.SaveChanges();

            // add check boxs
            _userprevli.id = _user.Id;
            _db.Users_Privileges.Add(_userprevli);

            _db.SaveChanges();


            return View();
        }
    }
}