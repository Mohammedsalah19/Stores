using Newtonsoft.Json;
using Stores.Models;
using Stores.Models.CommonClasses;
using Stores.Models.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stores.Controllers
{
    public class StoreHouseController : Controller
    {
        private ProjectContext _db = new ProjectContext();
        private Security s = new Security();



        #region product -secure


        #region index



        // GET: StoreHouse
        public ActionResult Index()
        {
            bool res = s.products();
            if (res == true)
            {
                var model = new BillsWithExten();
                model.prodCategoryX = _db.ProductCategory.ToList();
                model.ClientsX = _db.Clients.Where(f => f.Clients_Type_ID == 1).ToList();

                ViewBag.product = new SelectList(_db.Products.ToList(), "Pro_id", "name");
                ViewBag.productCat = new SelectList(_db.ProductCategory.ToList(), "Cate_ID", "name");
                return View(model);
            }

            return RedirectToAction("HavntAccess", "Employee");

        }

        #endregion


        #region fill table with products 

        public JsonResult FillTableWithProduct(string Pro_id)
        {

            List<Products> cat = new List<Products>();


            var getCatID = _db.ProductCategory.Where(ss => ss.name == Pro_id).FirstOrDefault();
            var obj = _db.Products.Where(p => p.Cate_ID == getCatID.Cate_ID).ToList();

            if (obj != null && obj.Count() > 0)
            {
                foreach (var item in obj)
                {
                    Products model = new Products();
                    model.Pro_id = item.Pro_id;
                    model.name = item.name;
                    model.barcode = item.barcode;
                    model.active = item.active;
                    model.Cate_ID = item.Cate_ID;
                    cat.Add(model);
                }
            }
            return Json(cat, JsonRequestBehavior.AllowGet);
        }


        #endregion

        #region return category name



        // return client product name fatoracontent

        public JsonResult ReturnCatName(int Cate_ID)
        {
            var model = _db.ProductCategory.Where(id => id.Cate_ID == Cate_ID).FirstOrDefault();
            return Json(model.name, JsonRequestBehavior.AllowGet);

        }

        #endregion


        #region  return active state 

        public JsonResult ReturnActive(int Pro_id)
        {
            var model = _db.Produt_Price.Where(id => id.Pro_ID == Pro_id).Select(f => f.active).FirstOrDefault();
            return Json(model, JsonRequestBehavior.AllowGet);

        }

        #endregion



        #region  edite record

        public JsonResult GetBillById(int Pro_id)
        {
            Produt_Price model = _db.Produt_Price.Where(x => x.Pro_ID == Pro_id).SingleOrDefault();
            string value = string.Empty;
            value = JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(value, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Tafeal and not 


        [HttpGet]
        public ActionResult GoEdit(int? Pro_id)
        {
            if (Pro_id == null)
            {
                return HttpNotFound();
            }

            var model = _db.Produt_Price.Where(f => f.Pro_ID == Pro_id).FirstOrDefault();
            var modelProduct = _db.Products.Where(f => f.Pro_id == Pro_id).FirstOrDefault();
            if (model == null)
            {
                return HttpNotFound();
            }

            if (model.active == true )
            {
                model.active = false;
                _db.Entry(model).State = EntityState.Modified;
                _db.SaveChanges();
                modelProduct.active = false;

                _db.Entry(modelProduct).State = EntityState.Modified;

                _db.SaveChanges();

                return RedirectToAction("index");

            }
            else
            {
                model.active = true;
                modelProduct.active = true;
                _db.Entry(model).State = EntityState.Modified;

                _db.SaveChanges();

                modelProduct.active = true;
                _db.Entry(modelProduct).State = EntityState.Modified;
                _db.SaveChanges();

                return RedirectToAction("index");

            }

        }



        //public JsonResult SaveDataInDatabase(Produt_Price model, bool active)
        //{
        //    var result = false;
        //    try
        //    {
        //        Produt_Price pro = _db.Produt_Price.SingleOrDefault(x => x.Prd_Pri_ID == model.Prd_Pri_ID);
        //        pro.cost = model.cost;
        //        pro.Price = model.Price;
        //        pro.Quantity = model.Quantity;
        //        pro.Minmum = model.Minmum;
        //        pro.Discount = model.Discount;
        //        pro.many_price = model.many_price;
        //        pro.active = model.active;

        //        pro.Pro_ID = model.Pro_ID;
        //        _db.SaveChanges();


        //        result = true;
        //    }

        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}
        #endregion

        #region  save new product

        public JsonResult SaveProduct(string Cat, string barcode, string name, decimal? cost, decimal? Price, decimal? Quantity, decimal? Minmum, decimal? Discount, bool active)
        {
            Products modelProduct = new Products();
            modelProduct.name = name;
            modelProduct.barcode = barcode;
            //  modelProduct.active = active ??default(bool);
            modelProduct.Cate_ID = _db.ProductCategory.Where(p => p.name == Cat).Select(f => f.Cate_ID).FirstOrDefault();

            _db.Products.Add(modelProduct);
            _db.SaveChanges();

            Produt_Price modelPrice = new Produt_Price();
            modelPrice.Pro_ID = modelProduct.Pro_id;

            modelPrice.cost = cost ?? default(decimal);
            modelPrice.Price = Price ?? default(decimal);
            modelPrice.Quantity = Quantity ?? default(decimal);
            modelPrice.Minmum = Minmum ?? default(decimal);
            modelPrice.Discount = Discount ?? default(decimal);
            modelPrice.active = active;
            _db.Produt_Price.Add(modelPrice);

            _db.SaveChanges();
            bool result = true;


            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #endregion


        #region Notification

        #region index

        public ActionResult Notification()
        {
            bool res = s.products();
            if (res == true)
            {
                var model = new NotificationWithExten();

                model.ProPricX = _db.Produt_Price.Where(p => p.Quantity <= p.Minmum).ToList();
                model.ProductX = _db.Products.ToList();
                model.StorehousesX = _db.Storehouse.ToList();

                return View(model);
            }
            return RedirectToAction("HavntAccess", "Employee");

        }

        #endregion


        #endregion


        #region  add stores


        #region index -secure

        public ActionResult Stores()
        {
            bool res = s.products();
            if (res == true)
            {
                var model = _db.Storehouse.ToList();
                return View(model);
            }
            return RedirectToAction("HavntAccess", "Employee");

        }
        #endregion


        #region add new store

        public JsonResult Addstore(Storehouse model)
        {

            var result = false;
            try
            {
                Storehouse newStore = new Storehouse();
                newStore.name = model.name;
                newStore.place = model.place;
                newStore.Description = model.Description;
                _db.Storehouse.Add(newStore);

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

        public JsonResult DeleteStore(int? Store_Id)
        {
            bool result = false;
            var store = _db.Storehouse.SingleOrDefault(x => x.Store_Id == Store_Id);
            if (store != null)
            {
                _db.Storehouse.Remove(store);

                _db.SaveChanges();

                result = true;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #endregion

        #region category


        #region index -secure

        public ActionResult Category()
        { bool res = s.products();
            if (res == true)
            {
                var model = _db.ProductCategory.ToList();

                return View(model);
            }
            return RedirectToAction("HavntAccess", "Employee");

        }

        #endregion



        #region add new cate

        public JsonResult AddCate(ProductCategory model)
        {

            var result = false;
            try
            {
                ProductCategory newCate = new ProductCategory();
                newCate.name = model.name;
                newCate.description = model.description;
                _db.ProductCategory.Add(newCate);

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

        public JsonResult DeleteCate(int? Cate_ID)
        {
            bool result = false;
            var cate = _db.ProductCategory.SingleOrDefault(x => x.Cate_ID == Cate_ID);
            if (cate != null)
            {
                _db.ProductCategory.Remove(cate);

                _db.SaveChanges();

                result = true;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion


        #endregion

    
    }

}