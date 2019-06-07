using Stores.Models;
using Stores.Models.CommonClasses;
using Stores.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;
namespace Stores.Controllers
{
    public class PaymentsController : Controller
    {

        private ProjectContext _db = new ProjectContext();
        private Security s = new Security();

        // GET: Payments
        public ActionResult Index(int? i)
        {
            bool res = s.Users();
            if (res == true)
            {
                var model = new PaymentsWithExten();
                model.ClientsX = _db.Clients.ToList();
                model.PaymentX = _db.Payments.ToList();
                model.UsersX = _db.Users.ToList();
                model.Clients_TypeX = _db.Clients_Type.ToList();

                return View(model);
            }
            return RedirectToAction("HavntAccess", "Employee");

        }
        #region retuern cat


        public JsonResult ClientsNames(string Clients_Type_id)
        {

            List<Clients> cat = new List<Clients>();


            var s = _db.Clients_Type.Where(ss => ss.name == Clients_Type_id).FirstOrDefault();
            var obj = _db.Clients.Where(p => p.Clients_Type_ID == s.Clients_Type_id).ToList();

            if (obj != null && obj.Count() > 0)
            {
                foreach (var item in obj)
                {
                    Clients model = new Clients();
                    model.Client_ID = item.Client_ID;
                    model.name = item.name;
                    cat.Add(model);
                }
            }

            return Json(cat, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region add Payment

        public JsonResult SavePaymentData(Payments model)
        {
            //bool result = true;
         

                try
                {
                    model.date = DateTime.Now;
                    //model.user_id = int.Parse(Session["userID"].ToString());
                    model.user_id = 50;

                    _db.Payments.Add(model);
                    _db.SaveChanges();
                }

                catch (Exception ex)
                {
                    throw ex;
                }
             

            return Json(JsonRequestBehavior.AllowGet);
        }


        #endregion

        

    }
}