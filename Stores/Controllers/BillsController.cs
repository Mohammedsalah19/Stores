using Newtonsoft.Json;
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
    public class BillsController : Controller
    {
        // GET: Bills
        private ProjectContext _db = new ProjectContext();

        public ActionResult Purchases()
        {
            var model = new BillsWithExten();
            //   model.billsX = _db.Bills.ToList();
            model.prodCategoryX = _db.ProductCategory.ToList();
            model.ClientsX = _db.Clients.ToList();

            //   model.productX = _db.Products.ToList();


            ViewBag.product = new SelectList(_db.Products.ToList(), "Pro_id", "name");
            ViewBag.productCat = new SelectList(_db.ProductCategory.ToList(), "Cate_ID", "name");
            return View(model);

        }

        #region fillBills

        public JsonResult FillBills()
        {
            List<BillsContent> list = new List<BillsContent>();

            var LastId = _db.BillsContent.OrderByDescending(u => u.Bill_ID).FirstOrDefault();

            var obj = _db.BillsContent.Where(ss => ss.Bill_ID == LastId.Bill_ID && ss.Status == false).ToList();

            // var obj = _db.Products.Where(p => p.Cate_ID == s.Cate_ID).ToList();

            if (obj != null && obj.Count() > 0)
            {
                foreach (var item in obj)
                {
                    BillsContent model = new BillsContent();

                    model.Product_ID = item.Product_ID;
                    model.BillsContent_ID = item.BillsContent_ID;
                    model.Price = item.Price;
                    model.Quantity = item.Quantity;
                    model.Status = item.Status;
                    list.Add(model);
                }
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region retuern cat


        public JsonResult GetcategoryPro(string Pro_id)
        {

            List<Products> cat = new List<Products>();


            var s = _db.ProductCategory.Where(ss => ss.name == Pro_id).FirstOrDefault();

            var obj = _db.Products.Where(p => p.Cate_ID == s.Cate_ID).ToList();

            if (obj != null && obj.Count() > 0)
            {
                foreach (var item in obj)
                {
                    Products model = new Products();
                    model.Pro_id = item.Pro_id;
                    model.name = item.name;
                    cat.Add(model);
                }
            }

            return Json(cat, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region add client

        public JsonResult SaveClientData(Clients model)
        {
            bool result = true;

            try
            {
                _db.Clients.Add(model);
                _db.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        #endregion

        #region return price and quntity value 

        public JsonResult ReturnValue(int Pro_id)
        {
            List<Produt_Price> result = new List<Produt_Price>();

            var model = _db.Produt_Price.Where(i => i.Pro_ID == 4061);
            // decimal result = model.Quantity;
            foreach (var item in model)
            {
                Produt_Price mo = new Produt_Price();
                mo.Prd_Pri_ID = item.Prd_Pri_ID;
                mo.Quantity = item.Quantity;
                mo.Minmum = item.Minmum;
                mo.cost = item.cost;
                mo.Price = item.Price;
                result.Add(mo);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region SaveBillData

        public JsonResult SaveBillData(BillsContent model)
        {
            bool result = true;

            try
            {
                _db.BillsContent.Add(model);
                _db.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region AddBillsContent 
        //add client id
        [HttpPost]
        public JsonResult AddBillsContent(BillsContent _BillContent, Bills _bill, int pro)
        {

            if (Session["flag"].ToString() == "true")
            {

                _bill.date = DateTime.Now;
                _bill.Client_ID = pro;
                _bill.User_ID = 5;

                _db.Bills.Add(_bill);
                _db.SaveChanges();
            }

            var LastId = _db.Bills.OrderByDescending(u => u.Id).FirstOrDefault();

            _BillContent.Bill_ID = LastId.Id;

            //    _BillContent.Price = price;
            _BillContent.Quantity = _BillContent.Quantity;

            _BillContent.Product_ID = pro;
            _db.BillsContent.Add(_BillContent);
            _db.SaveChanges();

            Session["flag"] = false;

            return Json(JsonRequestBehavior.AllowGet);
        }

        #endregion

        // gitt bill by id for edit

        #region  edite record

        public JsonResult GetBillById(int BillsContent_ID)
        {
            BillsContent model = _db.BillsContent.Where(x => x.BillsContent_ID == BillsContent_ID).SingleOrDefault();
            string value = string.Empty;
            value = JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(value, JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region  buttin for new fatora

        public ActionResult NewFatora()
        {
            Session["flag"] = "true";

            return RedirectToAction("Purchases");
        }

        #endregion

        #region Delete reord from data table

        public JsonResult DeleteContentRecord(int BillsContent_ID)
        {
            bool result = false;
            var Stu = _db.BillsContent.SingleOrDefault(x => x.BillsContent_ID == BillsContent_ID);
            if (Stu != null)
            {
                Stu.Status = true;
                _db.SaveChanges();
                result = true;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion


        public JsonResult returnLastid()
        {
             var LastId = _db.Bills.OrderByDescending(u => u.Id).FirstOrDefault();
             return Json(LastId.Id, JsonRequestBehavior.AllowGet);


        }
    }
}