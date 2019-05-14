using Newtonsoft.Json;
using Stores.Models;
using Stores.Models.CommonClasses;
using Stores.Models.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Stores.Controllers
{
    public class EmployeeController : Controller
    {
        private ProjectContext _db = new ProjectContext();
        private Security s = new Security();

        // GET: Employee

        #region index --secure

        public ActionResult Index()
        {
            var model = new UserAndPrivilage();
            model.UserX = _db.Users.ToList();
            model.PreviX = _db.Users_Privileges.ToList();

            bool res = s.Users();
            if (res == true)
            {

                return View(model);

            }
            return RedirectToAction("HavntAccess", "Employee");


        }
        #endregion

        #region Login

        [HttpGet]
        public ActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Login(Users _users)
        {

            var model = _db.Users.Where(p => p.username == _users.username && p.Password == _users.Password).FirstOrDefault();
            if (model != null)
            {
                Session["userName"] = model.username;
                Session["userID"] = model.Id;

                return RedirectToAction("Index", "Home");
            }
            Session["userName"] = null;
            return View();
        }
        #endregion

        #region Add Users --secure

        [HttpGet]
        public ActionResult NewEmployee()
        {
            bool res = s.Users();
            if (res == true)
            {
                return View();

            }
            return RedirectToAction("HavntAccess", "Employee");

        }


        [HttpPost]
        public ActionResult NewEmployee(Users _user, Users_Privileges _userprevli, string pic)
        {
            if (ModelState.IsValid)
            {

                string filName = Path.GetFileNameWithoutExtension(_user.Pic.FileName);

                string exten = Path.GetExtension(_user.Pic.FileName);
                filName = filName + DateTime.Now.ToString("yymmssff") + exten;
                _user.PicPath = @"\images\" + filName;


                filName = Path.Combine(Server.MapPath(@"~\images\"), filName);

                _user.Pic.SaveAs(filName);

                //     _user.user_current_date = DateTime.Now;
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

        #endregion

        #region Privilages --secure

        [HttpGet]
        public ActionResult Privilage(int? id)
        {
            bool res = s.Users();
            if (res == true)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Users_Privileges _userPrevi = _db.Users_Privileges.Find(id);

                Session["UserID"] = _userPrevi.User_ID;

                if (_userPrevi == null)
                {
                    return HttpNotFound();
                }

                return View(_userPrevi);
            }
            return RedirectToAction("HavntAccess", "Employee");


        }

        [HttpPost]
        public ActionResult Privilage(Users_Privileges _userPrevi)
        {
            string v = Session["UserID"].ToString();
            _userPrevi.User_ID = int.Parse(v);
            _db.Entry(_userPrevi).State = EntityState.Modified;
            _db.SaveChanges();
            TempData["editePrrivelage"] = "تم تعديل الصلاحيات بنجاح";
            return RedirectToAction("Index");
        }

        #endregion

        #region logOff


        public ActionResult LogOff(Users _users)
        {
            if (Session["userName"] != null)
            {
                Session.Clear();


            }
            return RedirectToAction("Login");
        }

        #endregion

        #region edit user --secure

        [HttpGet]
        public ActionResult UserEdit(int? id)
        {
            bool res = s.Users();
            if (res == true)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Users _user = _db.Users.Find(id);


                if (_user == null)
                {
                    return HttpNotFound();
                }

                return View(_user);
            }

            return RedirectToAction("HavntAccess", "Employee");
        }

        [HttpPost]
        public ActionResult UserEdit(Users _user)
        {
            //  _user.user_current_date = DateTime.Now;
            string filName = Path.GetFileNameWithoutExtension(_user.Pic.FileName);

            string exten = Path.GetExtension(_user.Pic.FileName);
            filName = filName + DateTime.Now.ToString("yymmssff") + exten;
            _user.PicPath = @"\images\" + filName;


            filName = Path.Combine(Server.MapPath(@"~\images\"), filName);

            _user.Pic.SaveAs(filName);


            _db.Entry(_user).State = EntityState.Modified;
            _db.SaveChanges();

            TempData["EditEmp"] = "تم التعديل معلومات الموظف";
            return RedirectToAction("Index");
        }
        #endregion

        #region json for edit


        public JsonResult GetStudentList()
        {
            List<Users> StuList = _db.Users.Where(x => x.active == true).Select(x => new Users
            {
                Id = x.Id,
                username = x.username,
                name = x.name,
                active = x.active,
                phone = x.phone,
                Password = x.Password,
                national_id = x.national_id,
                PicPath = x.PicPath,
                //user_current_date = x.user_current_date,

                //DepartmentName = x.tblDepartment.DepartmentName
            }).ToList();

            return Json(StuList, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetStudentById(int StudentId)
        {
            Users model = _db.Users.Where(x => x.Id == StudentId).SingleOrDefault();
            string value = string.Empty;
            value = JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(value, JsonRequestBehavior.AllowGet);
        }





        public JsonResult SaveDataInDatabase(Users model)
        {
            var result = false;
            try
            {
                if (model.Id > 0)
                {
                    var Stu = _db.Users.SingleOrDefault(x => x.Id == model.Id);
                    Stu.username = model.username;
                    Stu.name = model.name;
                    Stu.Password = model.Password;
                    Stu.national_id = model.national_id;
                    Stu.phone = model.phone;
                    Stu.PicPath = model.PicPath;
                    Stu.printer_name = model.printer_name;
                    //   Stu.user_current_date = DateTime.Now;
                    Stu.active = model.active;
                    _db.SaveChanges();
                    result = true;
                }
                else
                {
                    Users Stu = new Users();
                    Stu.username = model.username;
                    Stu.name = model.name;
                    Stu.Password = model.Password;
                    Stu.national_id = model.national_id;
                    Stu.phone = model.phone;
                    Stu.PicPath = model.PicPath;
                    Stu.printer_name = model.printer_name;
                    //   Stu.user_current_date = DateTime.Now;
                    Stu.active = model.active;
                    _db.Users.Add(Stu);
                    _db.SaveChanges();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        [HttpGet]

        public ActionResult Details(int?id)
        {
            bool res = s.Users();
            if (res == true)
            {
                if (id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var model = _db.Users.Find(id);

            if (model == null)
            {
                return HttpNotFound();
            }

            return View(model);
            }

            return RedirectToAction("HavntAccess", "Employee");

        }
        //[HttpPost]
        //public ActionResult Details(Users _user)
        //{
        //    return View(_user);

        //}





        #region Havent access


        [HttpGet]
        public ActionResult HavntAccess()
        {

            return View();

        }
        #endregion
    }
}