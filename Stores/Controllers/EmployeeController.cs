using CrystalDecisions.CrystalReports.Engine;
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
            model.PrintTypeX = _db.PrintType.ToList();

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

            var model = _db.Users.Where(p => p.username == _users.username && p.Password == _users.Password && p.active==true).FirstOrDefault();
            if (model != null)
            {
                Session["name"] = model.name;
                Session["userName"] = model.username;
                Session["userID"] = model.Id;
                Session["flag"] = "true";

                Session.Timeout = 720;
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
            ViewBag.PrintCat = new SelectList(_db.PrintType.ToList(), "ID", "PrinterName");

            bool res = s.Users();
            if (res == true)
            {
                return View();

            }
            return RedirectToAction("HavntAccess", "Employee");

        }


        [HttpPost]
        public ActionResult NewEmployee(Users _user, Users_Privileges _userprevli, string pic, string PrinterName)
        {
            if (ModelState.IsValid)
            {

                string filName = Path.GetFileNameWithoutExtension(_user.Pic.FileName);

                string exten = Path.GetExtension(_user.Pic.FileName);
                filName = filName + DateTime.Now.ToString("yymmssff") + exten;
                _user.PicPath = @"\images\" + filName;


                filName = Path.Combine(Server.MapPath(@"~\images\"), filName);

                _user.Pic.SaveAs(filName);

                _user.printer_name = PrinterName;
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

            ViewBag.PrintCat = new SelectList(_db.PrintType.ToList(), "ID", "PrinterName");

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
        public ActionResult UserEdit(Users _user, string PrinterName)
        {
            //  _user.user_current_date = DateTime.Now;
            string filName = Path.GetFileNameWithoutExtension(_user.Pic.FileName);

            string exten = Path.GetExtension(_user.Pic.FileName);
            filName = filName + DateTime.Now.ToString("yymmssff") + exten;
            _user.PicPath = @"\images\" + filName;


            filName = Path.Combine(Server.MapPath(@"~\images\"), filName);

            _user.Pic.SaveAs(filName);

            _user.printer_name = PrinterName;

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


        #region details

        [HttpGet]

        public ActionResult Details(int? id)
        {
            bool res = s.Users();
            if (res == true)
            {
                if (id == null)
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
        #endregion


        #region Printer type


        public ActionResult PrintType()
        {
            bool res = s.category();
            if (res == true)
            {
                var model = _db.PrintType.ToList();
                return View(model);
            }
            return RedirectToAction("HavntAccess", "Employee");

        }


        public JsonResult SavePrint(PrintType model)
        {
            bool result = true;

            _db.PrintType.Add(model);
            _db.SaveChanges();

            return Json(result, JsonRequestBehavior.AllowGet);
        }



        #endregion



        #region Delete record 

        public JsonResult DeletePrintType(int? ID)
             {
            bool result = false;
            var print = _db.PrintType.SingleOrDefault(x => x.ID == ID);
            if (print != null)
            {
                _db.PrintType.Remove(print);

                _db.SaveChanges();

                result = true;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region EndDay -- secure

        public DateTime EndOfDay(  DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 23, 59, 59, 999);
        }

        public   DateTime StartOfDay(  DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, 0);
        }


        public ActionResult EndDay()
        {
            bool res = s.endDay();
            if (res == true)
            {
                DateTime endday = EndOfDay(DateTime.Now);
                DateTime startday = StartOfDay(DateTime.Now);
                int userID = int.Parse(Session["userID"].ToString());

                var model = new EndDay();
                model.BillsX = _db.Bills.Where(d => d.date >= startday && d.date <= endday&& d.User_ID == userID).ToList();

                model.ExpensesX = _db.Expenses.Where(d => d.date >= startday && d.date <= endday &&d.User_ID== userID).ToList();
                model.ClientX = _db.Clients.ToList();
                model.PaymentsX = _db.Payments.Where(d => d.date >= startday && d.date <= endday &&d.user_id == userID).ToList();
                model.ExpensesTypeX = _db.ExpensesType.ToList();


                return View(model);
            }
            return RedirectToAction("HavntAccess", "Employee");

        }


        #endregion



        public ActionResult PrintEndDa()
        {

            int userID = int.Parse(Session["userID"].ToString());
            // printer name
            string PrinterID = _db.Users.Where(d => d.Id == userID).Select(f => f.printer_name).FirstOrDefault();
            string printerName = _db.PrintType.Where(id => id.ID.ToString() == PrinterID).Select(f => f.PrinterName).FirstOrDefault();


            DateTime endday = EndOfDay(DateTime.Now);
            DateTime startday = StartOfDay(DateTime.Now);
 
            var model = new EndDay();
            model.BillsX = _db.Bills.Where(d => d.date >= startday && d.date <= endday && d.User_ID == userID).ToList();

            model.ExpensesX = _db.Expenses.Where(d => d.date >= startday && d.date <= endday && d.User_ID == userID).ToList();
            model.ClientX = _db.Clients.ToList();

            model.PaymentsX = _db.Payments.Where(d => d.date >= startday && d.date <= endday && d.user_id == userID).ToList();
            model.ExpensesTypeX = _db.ExpensesType.ToList();


            var s = _db.Expenses.Where(d => d.date >= startday && d.date <= endday && d.User_ID == userID).Select(f => f.amount).Sum();

            decimal amount = 0;
            if (s==null)
            {
                amount = 0;
            }
            else
            {
                amount = s;
            }

            ReportDocument rd = new ReportDocument();

            rd.Load(Path.Combine(Server.MapPath("~/Report/EndDayReport.rpt")));
            Stream stream;



            rd.SetDataSource(_db.Bills.Select(p => new
            {
                date = endday,
                date2 = startday,
                User_ID = _db.Users.Where(f => f.Id == userID).Select(f => f.name).FirstOrDefault(),
                FatoraID = _db.Bills.Where(d => d.date >= startday && d.date <= endday && d.User_ID == userID).Count(),
                price = _db.Bills.Where(d => d.date >= startday && d.date <= endday && d.User_ID == userID).Select(f=>f.price).Sum(),
                discount = _db.Bills.Where(d => d.date >= startday && d.date <= endday && d.User_ID == userID).Select(f=>f.discount).Sum(),
                cost = _db.Bills.Where(d => d.date >= startday && d.date <= endday && d.User_ID == userID).Select(f=>f.cost).Sum(),
                comment = (_db.Bills.Where(d => d.date >= startday && d.date <= endday && d.User_ID == userID).Select(f => f.price).Sum() - _db.Bills.Where(d => d.date >= startday && d.date <= endday && d.User_ID == userID).Select(f => f.discount).Sum()) - _db.Bills.Where(d => d.date >= startday && d.date <= endday && d.User_ID == userID).Select(f => f.cost).Sum(),

                //expesnes
                 amount = _db.Expenses.Where(d => d.date >= startday && d.date <= endday && d.User_ID == userID).Select(f =>f.amount).Sum(),
                   //  amount = 520,
                     phone = _db.Expenses.Where(d => d.date >= startday && d.date <= endday && d.User_ID == userID).Count(),

             

                //payment
                Payments_ID = _db.Payments.Where(d => d.date >= startday && d.date <= endday && d.user_id == userID).Count(),
                 Payment_amount = _db.Payments.Where(d => d.date >= startday && d.date <= endday && d.user_id == userID).Select(f=>f.Payment_amount).Sum(),

          

                //info
                PicPath = _db.PLaceInfo.Select(f => f.Img).FirstOrDefault(),
                active = _db.PLaceInfo.Select(f => f.PlaceName).FirstOrDefault(),
                status = _db.PLaceInfo.Select(f => f.Number).FirstOrDefault(),




            }).ToList());
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);

            rd.PrintOptions.PrinterName = printerName;

            rd.PrintToPrinter(1, false, 0, 0);
            rd.Refresh();
            return RedirectToAction("EndDay");

            //   return File(stream, "aaplication/pdf", "تقرير انهاء اليوم.pdf");
        }

        #region Havent access


        [HttpGet]
        public ActionResult HavntAccess()
        {

            return View();

        }
        #endregion







        
    }
}