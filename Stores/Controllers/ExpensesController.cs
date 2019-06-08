using Stores.Models;
using Stores.Models.CommonClasses;
using Stores.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stores.Controllers
{
    public class ExpensesController : Controller
    {

        private ProjectContext _db = new ProjectContext();
        private Security s = new Security();


        // GET: Expenses
        public ActionResult Index()
        {
            var model = new ExpensesWithExten();


              model.ExpensesX=  _db.Expenses.ToList();
              model.ExpensesTypeX=  _db.ExpensesType.ToList();


            return View(model);
        }

        public ActionResult ExpensesType()
        {
            var model = _db.ExpensesType.ToList();
            return View(model);
        }




        #region add new ExpensesType


        public JsonResult AddExpensesType(ExpensesType model)
        {

            var result = false;
            try
            {
                ExpensesType newExpesne = new ExpensesType();
                newExpesne.name = model.name;
                newExpesne.comment = model.comment;
                _db.ExpensesType.Add(newExpesne);

                _db.SaveChanges();


                result = true;
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region Delete record 

        public JsonResult DeleteExpenseType(int? ExpensesType_ID)
        {
            bool result = false;
            var Expenses = _db.ExpensesType.SingleOrDefault(x => x.ExpensesType_ID == ExpensesType_ID);
            if (Expenses != null)
            {
                _db.ExpensesType.Remove(Expenses);

                _db.SaveChanges();

                result = true;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion



        #region add new ExpensesType


        public JsonResult AddExpenses(Expenses model)
        {

            var result = false;
            try
            {
                Expenses newExpesne = new Expenses();
                newExpesne.amount = model.amount;
                newExpesne.comment = model.comment;
                newExpesne.date = DateTime.Now;
                newExpesne.ExpensesType_ID = model.ExpensesType_ID;
                newExpesne.User_ID = model.User_ID;
                _db.Expenses.Add(newExpesne);

                _db.SaveChanges();


                result = true;
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion


        

    }
}