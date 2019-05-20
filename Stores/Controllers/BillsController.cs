﻿using Newtonsoft.Json;
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
    public class BillsController : Controller
    {
        // GET: Bills
        private ProjectContext _db = new ProjectContext();
        private Security s = new Security();

        #region index

        public ActionResult Purchases()
        {
            bool res = s.Users();
            if (res == true)
            {
                var model = new BillsWithExten();
                model.prodCategoryX = _db.ProductCategory.ToList();
                model.ClientsX = _db.Clients.ToList();



                ViewBag.product = new SelectList(_db.Products.ToList(), "Pro_id", "name");
                ViewBag.productCat = new SelectList(_db.ProductCategory.ToList(), "Cate_ID", "name");
                return View(model);
            }
            return RedirectToAction("HavntAccess", "Employee");


        }

        #endregion

        #region fillBills

        public JsonResult FillBills()
        {
            List<BillsContent> list = new List<BillsContent>();

            var LastId = _db.BillsContent.OrderByDescending(u => u.Bill_ID).FirstOrDefault();

            var obj = _db.BillsContent.Where(ss => ss.Bill_ID == LastId.Bill_ID && ss.IsDeleted == false && ss.Viewed==true).ToList();

            if (obj != null && obj.Count() > 0)
            {
                foreach (var item in obj)
                {
                    BillsContent model = new BillsContent();

                    model.Product_ID = item.Product_ID;
                    model.BillsContent_ID = item.BillsContent_ID;
                    model.Price = item.Price;
                    model.Quantity = item.Quantity;
                    model.IsDeleted = item.IsDeleted;
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

            var model = _db.Produt_Price.Where(i => i.Pro_ID == Pro_id);
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
        public ActionResult AddBillsContent(BillsContent _BillContent, Bills _bill, int pro, Produt_Price pro_price, int Quantity, string Client_ID )
        {
            bool res = s.Users();
            if (res == true)
            {
                if (Session["flag"].ToString() == "true")
                {
                    _bill.date = DateTime.Now;
                    // get client id
                    var ClintID = _db.Clients.Where(p => p.name == Client_ID).FirstOrDefault();
                    _bill.Client_ID = ClintID.Client_ID;

                    _bill.User_ID = int.Parse(Session["userID"].ToString());
                    _bill.Viewed = true;
                    _db.Bills.Add(_bill);
                    _db.SaveChanges();
                }

                var LastId = _db.Bills.OrderByDescending(u => u.Id).FirstOrDefault();
                _BillContent.Bill_ID = LastId.Id;

                //    _BillContent.Price = price;
                _BillContent.Quantity = _BillContent.Quantity;
                _BillContent.Product_ID = pro;
                _BillContent.Cost = _db.Produt_Price.Where(s => s.Pro_ID == pro).Select(p => p.cost).FirstOrDefault();
                _BillContent.Viewed = true;

                _db.BillsContent.Add(_BillContent);
                _db.SaveChanges();

                //edit quntity
                var proQuantity = _db.Produt_Price.Where(s => s.Pro_ID == pro).FirstOrDefault();
                Produt_Price model = _db.Produt_Price.Find(proQuantity.Prd_Pri_ID);

                model.Quantity = model.Quantity - Quantity;
                model.Store_Id = 1;
                model.Pro_ID = pro;
                model.Minmum = model.Minmum;
                model.many_price = model.many_price;
                model.Price = model.Price;
                model.cost = model.cost;


                _db.Entry(model).State = EntityState.Modified;
                _db.SaveChanges();

             
                Session["flag"] = "false";

                return RedirectToAction("Purchases");
            }

            return RedirectToAction("HavntAccess", "Employee");

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

        //public ActionResult NewFatora( decimal? discount, decimal? elmodfoa,Payments payments)
        //{


        //    decimal percent;
        //    decimal addeddiscount;

        //    //if (Discount.Contains("%"))
        //    //{

        //    //    percent = Convert.ToDecimal(Discount.Replace("%", "")) / 100;
        //    //    //addeddiscount = totalFatora * percent;



        //    //    var LastId = _db.Bills.OrderByDescending(u => u.Id).FirstOrDefault();
        //    //    Bills bill = _db.Bills.Find(LastId.Id);

        //    //    bill.price = _db.BillsContent.Where(s=>s.Bill_ID==LastId.Id).Sum(p=>p.Price);
        //    //    bill.cost = _db.BillsContent.Where(s => s.Bill_ID == LastId.Id).Sum(p => p.Cost);

        //    //    _db.Entry(bill).State = EntityState.Modified;
        //    //    _db.SaveChanges();

        //    //}
        //    //else
        //    //{
        //    //    try
        //    //    {
        //    var LastId = _db.Bills.OrderByDescending(u => u.Id).FirstOrDefault();
        //    Bills bill = _db.Bills.Find(LastId.Id);

        // //   addeddiscount = Convert.ToDecimal(Discount);

        //    decimal price = 0;
        //    decimal cost = 0;
        //    var model = _db.BillsContent.Where(s => s.Bill_ID == LastId.Id).ToList();
        //    foreach (var item in model)
        //    {
        //        price += item.Quantity * item.Price;
        //    }

        //    foreach (var item in model)
        //    {
        //        cost += item.Quantity * item.Cost;
        //    }

        //    bill.price = price;
        //    bill.cost = cost;
        //  bill.discount = discount ?? default(decimal);

        //    _db.Entry(bill).State = EntityState.Modified;
        //    _db.SaveChanges();


        //    payments.client_id = 1;
        //    payments.client_id = 1;
        //    payments.date = DateTime.Now;

        //    payments.Payment_amount = elmodfoa ?? default(decimal);

        //    _db.Payments.Add(payments);
        //    _db.SaveChanges();

        //    Session["flag"] = "true";

        //         return RedirectToAction("Purchases");
        //    }
        //    catch (Exception)
        //    {
        //        addeddiscount = 0;
        //    }
        //}


        //   


        //  }
        #endregion


        #region Newfatora

        public ActionResult Newfatora(decimal? discount, decimal? elmodfoa, Payments payments)
        {
            bool res = s.Users();
            if (res == true)
            {
                var LastId = _db.Bills.OrderByDescending(u => u.Id).FirstOrDefault();
                Bills bill = _db.Bills.Find(LastId.Id);
                var billCotent = _db.BillsContent.Where(s => s.Bill_ID == LastId.Id).ToList();

                foreach (var item in billCotent)
                {
                    item.Viewed = false;
                    _db.Entry(item).State = EntityState.Modified;
                }


                decimal price = 0;
                decimal cost = 0;
                var model = _db.BillsContent.Where(s => s.Bill_ID == LastId.Id).ToList();

                foreach (var item in model)
                {
                    price += item.Quantity * item.Price;
                }

                foreach (var item in model)
                {
                    cost += item.Quantity * item.Cost;
                }

                bill.price = price;
                bill.cost = cost;
                bill.discount = discount ?? default(decimal);
                bill.Viewed = false;
                _db.Entry(bill).State = EntityState.Modified;

                _db.SaveChanges();


                payments.client_id = 1;
                payments.client_id = 1;
                payments.date = DateTime.Now;

                payments.Payment_amount = elmodfoa ?? default(decimal);

                _db.Payments.Add(payments);
                _db.SaveChanges();

                Session["flag"] = "true";

                return RedirectToAction("Purchases");
            }

            return RedirectToAction("HavntAccess", "Employee");

        }
        #endregion


        #region Delete record from data table

        public JsonResult DeleteContentRecord(int BillsContent_ID, Produt_Price _proPrice)
        {
            bool result = false;
            var Stu = _db.BillsContent.SingleOrDefault(x => x.BillsContent_ID == BillsContent_ID);
            var model = _db.Produt_Price.Where(p => p.Pro_ID == Stu.Product_ID).FirstOrDefault();
            if (Stu != null)
            {
                Stu.IsDeleted = true;
                _db.SaveChanges();

                var editPrice = _db.Produt_Price.Find(model.Prd_Pri_ID);
                model.Quantity = model.Quantity + Stu.Quantity;

                _db.Entry(editPrice).State = EntityState.Modified;
                _db.SaveChanges();

                result = true;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region return last id in fatora 

        public JsonResult returnLastid()
        {
            var LastId = _db.Bills.OrderByDescending(u => u.Id).FirstOrDefault();
            return Json(LastId.Id, JsonRequestBehavior.AllowGet);


        }
        #endregion

        public ActionResult ReturnPurches()
        {

            return View(_db.BillsContent.ToList());
        }
      
    }

}