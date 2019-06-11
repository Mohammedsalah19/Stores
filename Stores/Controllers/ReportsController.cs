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
    public class ReportsController : Controller
    {
        private ProjectContext _db = new ProjectContext();
        private Security s = new Security();


        // GET: Reports

        #region fawater

        #region index

        public ActionResult fwater()
        {

            bool res = s.statistics();
            if (res == true)
            {

                var model = new BillsWithExten();

                model.billsX = _db.Bills.ToList();
                model.ClientsX = _db.Clients.ToList();
                model.UserX = _db.Users.ToList();
                model.BillcateX = _db.BillsCategory.ToList();

                return View(model);
            }
            return RedirectToAction("HavntAccess", "Employee");


        }
        #endregion

        #region search

        public JsonResult Getsearch(DateTime from, DateTime to, string BillCat, string clientName, string UserName)
        {

            var BillCatID = _db.BillsCategory.Where(p => p.name == BillCat).Select(f => f.BillCate_ID).FirstOrDefault();
            var clientNameID = _db.Clients.Where(p => p.name == clientName).Select(f => f.Client_ID).FirstOrDefault();
            var UserNameID = _db.Users.Where(p => p.name == UserName).Select(f => f.Id).FirstOrDefault();

            if (BillCatID != 0 && clientNameID != 0 && UserNameID != 0)
            {
                var modelBillcat = _db.Bills.Where(d => d.date >= from && d.date <= to && d.Cate_Id == BillCatID && d.User_ID == UserNameID && d.Client_ID == clientNameID).ToList();
                return Json(modelBillcat, JsonRequestBehavior.AllowGet);

            }
            else if (BillCatID != 0 && clientNameID != 0 && UserNameID == 0)


            {
                var modelBillcat = _db.Bills.Where(d => d.date >= from && d.date <= to && d.Cate_Id == BillCatID && d.Client_ID == clientNameID).ToList();
                return Json(modelBillcat, JsonRequestBehavior.AllowGet);

            }
            else if (BillCatID != 0 && clientNameID == 0 && UserNameID != 0)


            {
                var modelBillcat = _db.Bills.Where(d => d.date >= from && d.date <= to && d.Cate_Id == BillCatID && d.User_ID == UserNameID).ToList();
                return Json(modelBillcat, JsonRequestBehavior.AllowGet);

            }
            else if (BillCatID == 0 && clientNameID != 0 && UserNameID != 0)


            {
                var modelBillcat = _db.Bills.Where(d => d.date >= from && d.date <= to && d.User_ID == UserNameID && d.Client_ID == clientNameID).ToList();
                return Json(modelBillcat, JsonRequestBehavior.AllowGet);

            }

            else if (BillCatID != 0 && clientNameID == 0 && UserNameID == 0)


            {
                var modelBillcat = _db.Bills.Where(d => d.date >= from && d.date <= to && d.Cate_Id == BillCatID).ToList();
                return Json(modelBillcat, JsonRequestBehavior.AllowGet);

            }
            else if (BillCatID == 0 && clientNameID != 0 && UserNameID == 0)


            {
                var modelBillcat = _db.Bills.Where(d => d.date >= from && d.date <= to && d.Client_ID == clientNameID).ToList();
                return Json(modelBillcat, JsonRequestBehavior.AllowGet);

            }
            else if (BillCatID == 0 && clientNameID == 0 && UserNameID != 0)


            {
                var modelBillcat = _db.Bills.Where(d => d.date >= from && d.date <= to && d.User_ID == UserNameID).ToList();
                return Json(modelBillcat, JsonRequestBehavior.AllowGet);

            }


            var model = _db.Bills.Where(d => d.date >= from && d.date <= to).ToList();
            return Json(model, JsonRequestBehavior.AllowGet);
        }


        public JsonResult ReturnClientName(int Client_ID)
        {

            var modelClient = _db.Clients.Where(id => id.Client_ID == Client_ID).FirstOrDefault();
            return Json(modelClient, JsonRequestBehavior.AllowGet);

        }


        public JsonResult ReturnUserName(int User_ID)
        {

            var modelClient = _db.Users.Where(id => id.Id == User_ID).FirstOrDefault();
            return Json(modelClient, JsonRequestBehavior.AllowGet);

        }

        public JsonResult ReturnCateName(int Cate_Id)
        {

            var modelClient = _db.BillsCategory.Where(id => id.BillCate_ID == Cate_Id).FirstOrDefault();
            return Json(modelClient, JsonRequestBehavior.AllowGet);

        }

        #endregion

        #endregion



        public ActionResult Products()
        {


            var model = new BillsWithExten();

            model.productX = _db.Products.ToList();
            model.prodCategoryX = _db.ProductCategory.ToList();
            model.prodPriceX = _db.Produt_Price.ToList();
            model.billCOntentX = _db.BillsContent.ToList();
            return View(model);
        }






        public JsonResult GetsearchProducr(DateTime from, DateTime to, string BillCat)
        {

            var BillCatID = _db.BillsCategory.Where(p => p.name == BillCat).Select(f => f.BillCate_ID).FirstOrDefault();

            var model = _db.Bills.Where(d => d.date >= from && d.date <= to).ToList();
            List<int> list = new List<int>();

            List<Products> list2 = new List<Products>();

            IEnumerable<BillsContent> s;
            foreach (var item in model)
            {

                BillsContent _BillContent = new BillsContent();
                var models = _db.BillsContent.Where(p => p.Bill_ID == item.Id).ToList();

                foreach (var items in models.Select(f=>f.Product_ID).Distinct())
                {
                    list.Add(items);
                }
            }

            foreach (var item in list)
            {
                var res = _db.Products.Where(p => p.Pro_id == item);
                foreach (var items in res)
                {
                    list2.Add(items);

                }
            }
        //    var res = _db.Products.Where(p=>p.Pro_id==)
            return Json(list2, JsonRequestBehavior.AllowGet);
        }



        


        public JsonResult ReturnCateNameProduct(int Cate_Id)
        {

            var res = _db.ProductCategory.Where(id => id.Cate_ID == Cate_Id).FirstOrDefault();
            return Json(res, JsonRequestBehavior.AllowGet);

        }


        public JsonResult getQuntity(int Pro_id)
        {
 
            var res = _db.BillsContent.Where(id => id.Product_ID == Pro_id).Sum(f=>f.Quantity);
            return Json(res, JsonRequestBehavior.AllowGet);

        }
        
    }
}