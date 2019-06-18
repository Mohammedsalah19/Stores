using CrystalDecisions.CrystalReports.Engine;
using Stores.Models;
using Stores.Models.CommonClasses;
using Stores.Models.DAL;
using System;
using System.Collections.Generic;
using System.IO;
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

        #region fawater --secure

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


        #region products --secure



        #region index

        public ActionResult Products()
        {

            bool res = s.statistics();
            if (res == true)
            {
                var model = new BillsWithExten();

                model.productX = _db.Products.ToList();
                model.prodCategoryX = _db.ProductCategory.ToList();
                model.prodPriceX = _db.Produt_Price.ToList();
                model.billCOntentX = _db.BillsContent.ToList();
                model.BillcateX = _db.BillsCategory.ToList();
                return View(model);

            }
            return RedirectToAction("HavntAccess", "Employee");

        }

        #endregion


        #region search



        public JsonResult GetsearchProduct(DateTime from, DateTime to, string BillCat)
        {

            var BillCatID = _db.BillsCategory.Where(p => p.name == BillCat).Select(f => f.BillCate_ID).FirstOrDefault();

            if (BillCatID != 0)
            {
                var model = _db.Bills.Where(d => d.date >= from && d.date <= to && d.Cate_Id == BillCatID).ToList();


                List<int> list = new List<int>();

                List<Products> list2 = new List<Products>();

                foreach (var item in model)
                {

                    BillsContent _BillContent = new BillsContent();
                    var models = _db.BillsContent.Where(p => p.Bill_ID == item.Id).ToList();

                    foreach (var items in models.Select(f => f.Product_ID))
                    {
                        list.Add(items);
                    }
                }

                foreach (var item in list.Distinct())
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
            else
            {




                var model = _db.Bills.Where(d => d.date >= from && d.date <= to).ToList();


                List<int> list = new List<int>();

                List<Products> list2 = new List<Products>();

                IEnumerable<BillsContent> s;
                foreach (var item in model)
                {

                    BillsContent _BillContent = new BillsContent();
                    var models = _db.BillsContent.Where(p => p.Bill_ID == item.Id).ToList();

                    foreach (var items in models.Select(f => f.Product_ID))
                    {
                        list.Add(items);
                    }
                }

                foreach (var item in list.Distinct())
                {
                    var res = _db.Products.Where(p => p.Pro_id == item);
                    foreach (var items in res)
                    {
                        list2.Add(items);

                    }
                }
                return Json(list2, JsonRequestBehavior.AllowGet);
            }

        }



        #endregion

        #region get jquery fun

        public JsonResult ReturnCateNameProduct(int Cate_Id)
        {

            var res = _db.ProductCategory.Where(id => id.Cate_ID == Cate_Id).FirstOrDefault();
            return Json(res, JsonRequestBehavior.AllowGet);

        }


        public JsonResult getQuntity(int Pro_id)
        {

            var res = _db.BillsContent.Where(id => id.Product_ID == Pro_id).Sum(f => f.Quantity);
            return Json(res, JsonRequestBehavior.AllowGet);

        }

        public JsonResult getPrice(int Pro_id)
        {

            var res = _db.Produt_Price.Where(id => id.Pro_ID == Pro_id).Select(f => f.Price).FirstOrDefault();
            return Json(res, JsonRequestBehavior.AllowGet);

        }

        public JsonResult getCost(int Pro_id)
        {

            var res = _db.Produt_Price.Where(id => id.Pro_ID == Pro_id).Select(f => f.cost).FirstOrDefault();
            return Json(res, JsonRequestBehavior.AllowGet);

        }

        public JsonResult getMaksab(int Pro_id)
        {

            var cost = _db.Produt_Price.Where(id => id.Pro_ID == Pro_id).Select(f => f.cost).FirstOrDefault();
            var price = _db.Produt_Price.Where(id => id.Pro_ID == Pro_id).Select(f => f.Price).FirstOrDefault();

            var res = price - cost;
            return Json(res, JsonRequestBehavior.AllowGet);

        }


        #endregion

        #endregion

        #region clients --secure


        #region index


        public ActionResult clients()
        {
            bool res = s.statistics();
            if (res == true)
            {
                var model = new ClientWithExten();

                model.ClientsX = _db.Clients.ToList();
                model.Clients_Type = _db.Clients_Type.ToList();

                return View(model);
            }
            return RedirectToAction("HavntAccess", "Employee");


        }

        #endregion
        #region retuern cat


        public JsonResult ClientCat(string Clients_Type_id)
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

        #region seacrch


        public JsonResult ClientsReport(DateTime from, DateTime to, int client)
        {

            List<string> list = new List<string>();

            var modelBillcat = _db.Bills.Where(d => d.date >= from && d.date <= to && d.Client_ID == client).ToList();


            return Json(modelBillcat, JsonRequestBehavior.AllowGet);


        }

        #endregion

        #region client jquery fun

        public JsonResult getClintName(int Client_ID)
        {

            var res = _db.Clients.Where(id => id.Client_ID == Client_ID).FirstOrDefault();
            return Json(res, JsonRequestBehavior.AllowGet);

        }
        public JsonResult getFawaterNumbers(DateTime from, DateTime to, int Client_ID)
        {

            var res = _db.Bills.Where(d => d.date >= from && d.date <= to && d.Client_ID == Client_ID).Count();
            return Json(res, JsonRequestBehavior.AllowGet);

        }
        public JsonResult getFwaterSum(DateTime from, DateTime to, int Client_ID)
        {

            var res = _db.Bills.Where(d => d.date >= from && d.date <= to && d.Client_ID == Client_ID).Sum(f => f.price);
            return Json(res, JsonRequestBehavior.AllowGet);

        }
        public JsonResult getPayments(DateTime from, DateTime to, int Client_ID)
        {

            var res = _db.Payments.Where(d => d.date >= from && d.date <= to && d.client_id == Client_ID).Sum(f => f.Payment_amount);
            return Json(res, JsonRequestBehavior.AllowGet);

        }
        public JsonResult getElmtbaqy(DateTime from, DateTime to, int Client_ID)
        {

            var res = _db.Bills.Where(d => d.date >= from && d.date <= to && d.Client_ID == Client_ID).Sum(f => f.price);

            var res2 = _db.Payments.Where(d => d.date >= from && d.date <= to && d.client_id == Client_ID).Sum(f => f.Payment_amount);
            var final = res - res2;
            return Json(final, JsonRequestBehavior.AllowGet);

        }

        #endregion


        #endregion



        #region expenses -- secure



        #region index


        public ActionResult Expenses()
        {

            bool res = s.statistics();
            if (res == true)
            {
                var model = new ExpensesWithExten();


                model.ExpensesTypeX = _db.ExpensesType.ToList();
                model.ExpensesX = _db.Expenses.ToList();
                model.UsersX = _db.Users.ToList();
                return View(model);

            }
            return RedirectToAction("HavntAccess", "Employee");


        }

        #endregion

        #region search


        public JsonResult GetsearchExpenses(DateTime from, DateTime to, string ExpenesCat, string Client)
        {

            var usrId = _db.Users.Where(d => d.name == Client).Select(f => f.Id).FirstOrDefault();
            var CateExpId = _db.ExpensesType.Where(d => d.name == ExpenesCat).Select(f => f.ExpensesType_ID).FirstOrDefault();

            if (CateExpId != 0 && usrId != 0)
            {
                var model = _db.Expenses.Where(d => d.date >= from && d.date <= to && d.ExpensesType_ID == CateExpId && d.User_ID == usrId).ToList();
                return Json(model, JsonRequestBehavior.AllowGet);

            }
            if (CateExpId != 0 && usrId == 0)
            {
                var model = _db.Expenses.Where(d => d.date >= from && d.date <= to && d.ExpensesType_ID == CateExpId).ToList();
                return Json(model, JsonRequestBehavior.AllowGet);

            }
            if (CateExpId == 0 && usrId != 0)
            {
                var modelBillcat = _db.Expenses.Where(d => d.date >= from && d.date <= to && d.User_ID == usrId).ToList();
                return Json(modelBillcat, JsonRequestBehavior.AllowGet);

            }
            else
            {


                var model = _db.Expenses.Where(d => d.date >= from && d.date <= to).ToList();
                return Json(model, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion



        #region jquery fun



        public JsonResult getExpenseName(int ExpensesType_ID)
        {

            var res = _db.ExpensesType.Where(id => id.ExpensesType_ID == ExpensesType_ID).FirstOrDefault();
            return Json(res, JsonRequestBehavior.AllowGet);

        }

        public JsonResult getUserName(int User_ID)
        {

            var res = _db.Users.Where(id => id.Id == User_ID).FirstOrDefault();
            return Json(res, JsonRequestBehavior.AllowGet);

        }
        #endregion


        #endregion



        #region Payments --secure


        #region index

        public ActionResult payments()
        {
            bool res = s.statistics();
            if (res == true)
            {
                var model = new PaymentsWithExten();
                model.PaymentX = _db.Payments.ToList();
                model.ClientsX = _db.Clients.ToList();
                model.Clients_TypeX = _db.Clients_Type.ToList();
                return View(model);
            }
            return RedirectToAction("HavntAccess", "Employee");


        }
        #endregion

        #region search

        public JsonResult Paymentsreport(DateTime from, DateTime to, string client)
        {

            if (client == "")
            {
                var model = _db.Payments.Where(d => d.date >= from && d.date <= to).ToList();
                return Json(model, JsonRequestBehavior.AllowGet);


            }
            else
            {
                var model = _db.Payments.Where(d => d.date >= from && d.date <= to && d.client_id.ToString() == client).ToList();
                return Json(model, JsonRequestBehavior.AllowGet);

            }

        }
        #endregion


        #region Jquery fun


        public JsonResult getclientName(int client_id)
        {

            var res = _db.Clients.Where(id => id.Client_ID == client_id).FirstOrDefault();
            return Json(res, JsonRequestBehavior.AllowGet);

        }
        #endregion


        #endregion



        public ActionResult BillsReort()
        {

            ReportDocument rd = new ReportDocument();
 
            rd.Load(Path.Combine(Server.MapPath("~/Report/BillReport.rpt")));

            //rd.SetDataSource(_db.Bills.Select(p => new
            //{
            //    id = p.Id,
            //    UserName1 = _db.Users.Where(f => f.Id == p.User_ID).Select(f => f.name).FirstOrDefault(),
            //    ClientName = _db.Clients.Where(f => f.Client_ID == p.Client_ID).Select(f => f.name).FirstOrDefault(),
            //    date = p.date,
            //    BillCate_ID = _db.BillsCategory.Where(f => f.BillCate_ID == p.Cate_Id).Select(f => f.name).FirstOrDefault(),
            //    price = p.price,
            //    discount = p.discount,
            //    cost = p.cost,
            //    phone =p.price - p.cost,

            //}).ToList());



            rd.SetDataSource(_db.Bills.Select(p => new
            {
                 
                Comment = _db.BillsCategory.Where(d=>d.BillCate_ID==p.Cate_Id).Select(f=>f.name).FirstOrDefault(),
                price = _db.Bills.Select(f=>f.price).Sum(),
                cost = _db.Bills.Select(f=>f.cost).Sum(),
                discount = _db.Bills.Select(f=>f.discount).Sum(),
                PicPath = p.price - p.cost,

            }).ToList());
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "aaplication/pdf", "سجل الفواتير.pdf");

         }


    }

}