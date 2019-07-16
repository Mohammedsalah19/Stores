using Stores.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stores.Controllers
{
    public class AdminController : Controller
    {
        private ProjectContext _db = new ProjectContext();
        private Security s = new Security();

        // GET: Admin
        public ActionResult Index()
        {
            if (Session["AdminName"]== null)
            {
                return RedirectToAction("login");
            }
            return View();
        }

        #region delete all 

        [HttpGet]
        public ActionResult delletall()
        {
            if (Session["AdminName"] != null)
            {
                var all = _db.Tables.ToList();

                return View(all);
            }
            return RedirectToAction("login");


        }

        #endregion

        public JsonResult DeleteAllTables(int Tablevalues)
        {

            var res = _db.Tables.Where(f => f.ID == Tablevalues).Select(g => g.values).FirstOrDefault();

            if (res == "DelBills")
            {
                var all = _db.Bills.ToList();
                foreach (var item in all)
                {
                    _db.Bills.Remove(item);
                    _db.SaveChanges();
                }

       
            }

            if (res == "DelContent")
            {
                var all = _db.BillsContent.ToList();
                foreach (var item in all)
                {
                    _db.BillsContent.Remove(item);
                    _db.SaveChanges();
                }
            }

            if (res == "")
            {
                var all = _db.BillsCategory.ToList();
                foreach (var item in all)
                {
                    _db.BillsCategory.Remove(item);
                    _db.SaveChanges();
                }
            }

            if (res == "DelBills")
            {
                var all = _db.Bills.ToList();
                foreach (var item in all)
                {
                    _db.Bills.Remove(item);
                    _db.SaveChanges();
                }
            }

            if (res == "DelClient")
            {
                var all = _db.Clients.ToList();
                foreach (var item in all)
                {
                    _db.Clients.Remove(item);
                    _db.SaveChanges();
                }
            }

            if (res == "DelCleintCat")
            {
                var all = _db.Clients_Type.ToList();
                foreach (var item in all)
                {
                    _db.Clients_Type.Remove(item);
                    _db.SaveChanges();
                }
            }

            if (res == "DelBills")
            {
                var all = _db.Bills.ToList();
                foreach (var item in all)
                {
                    _db.Bills.Remove(item);
                    _db.SaveChanges();
                }
            }

            if (res == "DelExpenses")
            {
                var all = _db.Expenses.ToList();
                foreach (var item in all)
                {
                    _db.Expenses.Remove(item);
                    _db.SaveChanges();
                }
            }

            if (res == "DelExpensesCat")
            {
                var all = _db.ExpensesType.ToList();
                foreach (var item in all)
                {
                    _db.ExpensesType.Remove(item);
                    _db.SaveChanges();
                }
            }
            if (res == "DelPayments")
            {
                var all = _db.Payments.ToList();
                foreach (var item in all)
                {
                    _db.Payments.Remove(item);
                    _db.SaveChanges();
                }
            }
            if (res == "DelInfo")
            {
                var all = _db.PLaceInfo.ToList();
                foreach (var item in all)
                {
                    _db.PLaceInfo.Remove(item);
                    _db.SaveChanges();
                }
            }
            if (res == "DelProducts")
            {
                var all = _db.Products.ToList();
                foreach (var item in all)
                {
                    _db.Products.Remove(item);
                    _db.SaveChanges();
                }
            }
            if (res == "DelProductCat")
            {
                var all = _db.ProductCategory.ToList();
                foreach (var item in all)
                {
                    _db.ProductCategory.Remove(item);
                    _db.SaveChanges();
                }
            }

            if (res == "DelProductPrice")
            {
                var all = _db.Produt_Price.ToList();
                foreach (var item in all)
                {
                    _db.Produt_Price.Remove(item);
                    _db.SaveChanges();
                }
            }

            if (res == "DelProduthouse")
            {
                var all = _db.Storehouse.ToList();
                foreach (var item in all)
                {
                    _db.Storehouse.Remove(item);
                    _db.SaveChanges();
                }
            }

            if (res == "DelUsers")
            {
                var all = _db.Users.ToList();
                foreach (var item in all)
                {
                    _db.Users.Remove(item);
                    _db.SaveChanges();
                }
            }

            if (res == "DelPrivilage")
            {
                var all = _db.Users_Privileges.ToList();
                foreach (var item in all)
                {
                    _db.Users_Privileges.Remove(item);
                    _db.SaveChanges();
                }
            }



            return Json(JsonRequestBehavior.AllowGet);

        }








        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string email , string pass)
        {
            if (email == "uptop" && pass == "11")
            {
                Session["AdminName"] = email;
                return RedirectToAction("Index");
            }
            return View();
        }

    }
}