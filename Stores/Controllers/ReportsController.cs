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


        public JsonResult getQuntity(int Pro_id, DateTime from, DateTime to, string BillCat)
        {
            List<BillsContent> list = new List<BillsContent>();
            var BillCatID = _db.BillsCategory.Where(p => p.name == BillCat).Select(f => f.BillCate_ID).FirstOrDefault();

            var model = _db.Bills.Where(d => d.date >= from && d.date <= to && d.Cate_Id== BillCatID).ToList();
            foreach (var item in model)
            {

                var models = _db.BillsContent.Where(p => p.Bill_ID == item.Id).ToList();

                foreach (var items in models)
                {
                    list.Add(items);
                }
            }
            var s = list.Where(f => f.Product_ID == Pro_id).Select(f => f.Quantity).Sum();
             //var res = _db.BillsContent.Where(id => id.Product_ID == Pro_id).Sum(f => f.Quantity);
            return Json(s, JsonRequestBehavior.AllowGet);

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

        public JsonResult getMaksab(int Pro_id, DateTime from, DateTime to)
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


        #region print bills

        public ActionResult BillsReort(DateTime from, DateTime to, string BillCat, string clientName, string UserName)
        {

            ReportDocument rd = new ReportDocument();

            rd.Load(Path.Combine(Server.MapPath("~/Report/billStatistics.rpt")));
            Stream stream;


            var BillCatID = _db.BillsCategory.Where(p => p.name == BillCat).Select(f => f.BillCate_ID).FirstOrDefault();
            var clientNameID = _db.Clients.Where(p => p.name == clientName).Select(f => f.Client_ID).FirstOrDefault();
            var UserNameID = _db.Users.Where(p => p.name == UserName).Select(f => f.Id).FirstOrDefault();

            if (BillCatID != 0 && clientNameID != 0 && UserNameID != 0)
            {
                var modelBillcat = _db.Bills.Where(d => d.date >= from && d.date <= to && d.Cate_Id == BillCatID && d.User_ID == UserNameID && d.Client_ID == clientNameID).ToList();
                rd.SetDataSource(modelBillcat.Select(p => new
                {
                    id = p.Id,
                    Password = from,
                    phone = to,
                    Cate_Id = _db.BillsCategory.Where(d => d.BillCate_ID == BillCatID).Select(f => f.name).FirstOrDefault(),

                    price = modelBillcat.Select(f => f.price).Sum(),
                    cost = modelBillcat.Select(f => f.cost).Sum(),
                    discount = modelBillcat.Select(f => f.discount).Sum(),
                    name = modelBillcat.Select(f => f.price).Sum() - modelBillcat.Select(f => f.discount).Sum(),
                    national_id = (modelBillcat.Select(f => f.price).Sum() - modelBillcat.Select(f => f.discount).Sum()) - modelBillcat.Select(f => f.cost).Sum(),

                    User_ID = _db.Users.Where(d => d.Id == p.User_ID).First(),
                    Client_ID = _db.Clients.Where(d => d.Client_ID == p.Client_ID).First(),
                    Comment = _db.BillsCategory.Where(d => d.BillCate_ID == p.Cate_Id).Select(f => f.name).FirstOrDefault(),
                    //info
                    Viewed = _db.PLaceInfo.Select(f => f.Img).FirstOrDefault(),
                    active = _db.PLaceInfo.Select(f => f.PlaceName).FirstOrDefault(),
                    status = _db.PLaceInfo.Select(f => f.Number).FirstOrDefault(),

                }).ToList());
                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "aaplication/pdf", "سجل الفواتير.pdf");
            }
            else if (BillCatID != 0 && clientNameID != 0 && UserNameID == 0)


            {
                var modelBillcat = _db.Bills.Where(d => d.date >= from && d.date <= to && d.Cate_Id == BillCatID && d.Client_ID == clientNameID).ToList();
                rd.SetDataSource(modelBillcat.Select(p => new
                {
                    id = p.Id,
                    Password = from,
                    phone = to,
                    Cate_Id = _db.BillsCategory.Where(d => d.BillCate_ID == BillCatID).Select(f => f.name).FirstOrDefault(),

                    price = modelBillcat.Select(f => f.price).Sum(),
                    cost = modelBillcat.Select(f => f.cost).Sum(),
                    discount = modelBillcat.Select(f => f.discount).Sum(),
                    name = modelBillcat.Select(f => f.price).Sum() - modelBillcat.Select(f => f.discount).Sum(),
                    national_id = (modelBillcat.Select(f => f.price).Sum() - modelBillcat.Select(f => f.discount).Sum()) - modelBillcat.Select(f => f.cost).Sum(),

                    User_ID = _db.Users.Where(d => d.Id == p.User_ID).First(),
                    Client_ID = _db.Clients.Where(d => d.Client_ID == p.Client_ID).First(),
                    Comment = _db.BillsCategory.Where(d => d.BillCate_ID == p.Cate_Id).Select(f => f.name).FirstOrDefault(),
                    //info
                    Viewed = _db.PLaceInfo.Select(f => f.Img).FirstOrDefault(),
                    active = _db.PLaceInfo.Select(f => f.PlaceName).FirstOrDefault(),
                    status = _db.PLaceInfo.Select(f => f.Number).FirstOrDefault(),

                }).ToList());
                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "aaplication/pdf", "سجل الفواتير.pdf");
            }
            else if (BillCatID != 0 && clientNameID == 0 && UserNameID != 0)


            {
                var modelBillcat = _db.Bills.Where(d => d.date >= from && d.date <= to && d.Cate_Id == BillCatID && d.User_ID == UserNameID).ToList();
                rd.SetDataSource(modelBillcat.Select(p => new
                {
                    id = p.Id,
                    Password = from,
                    phone = to,
                    Cate_Id = _db.BillsCategory.Where(d => d.BillCate_ID == BillCatID).Select(f => f.name).FirstOrDefault(),

                    price = modelBillcat.Select(f => f.price).Sum(),
                    cost = modelBillcat.Select(f => f.cost).Sum(),
                    discount = modelBillcat.Select(f => f.discount).Sum(),
                    name = modelBillcat.Select(f => f.price).Sum() - modelBillcat.Select(f => f.discount).Sum(),
                    national_id = (modelBillcat.Select(f => f.price).Sum() - modelBillcat.Select(f => f.discount).Sum()) - modelBillcat.Select(f => f.cost).Sum(),

                    User_ID = _db.Users.Where(d => d.Id == p.User_ID).First(),
                    Client_ID = _db.Clients.Where(d => d.Client_ID == p.Client_ID).First(),
                    Comment = _db.BillsCategory.Where(d => d.BillCate_ID == p.Cate_Id).Select(f => f.name).FirstOrDefault(),
                    //info
                    Viewed = _db.PLaceInfo.Select(f => f.Img).FirstOrDefault(),
                    active = _db.PLaceInfo.Select(f => f.PlaceName).FirstOrDefault(),
                    status = _db.PLaceInfo.Select(f => f.Number).FirstOrDefault(),

                }).ToList());
                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "aaplication/pdf", "سجل الفواتير.pdf");
            }
            else if (BillCatID == 0 && clientNameID != 0 && UserNameID != 0)


            {
                var modelBillcat = _db.Bills.Where(d => d.date >= from && d.date <= to && d.User_ID == UserNameID && d.Client_ID == clientNameID).ToList();
                rd.SetDataSource(modelBillcat.Select(p => new
                {
                    id = p.Id,
                    Password = from,
                    phone = to,
                    Cate_Id = _db.BillsCategory.Where(d => d.BillCate_ID == BillCatID).Select(f => f.name).FirstOrDefault(),

                    price = modelBillcat.Select(f => f.price).Sum(),
                    cost = modelBillcat.Select(f => f.cost).Sum(),
                    discount = modelBillcat.Select(f => f.discount).Sum(),
                    name = modelBillcat.Select(f => f.price).Sum() - modelBillcat.Select(f => f.discount).Sum(),
                    national_id = (modelBillcat.Select(f => f.price).Sum() - modelBillcat.Select(f => f.discount).Sum()) - modelBillcat.Select(f => f.cost).Sum(),

                    User_ID = _db.Users.Where(d => d.Id == p.User_ID).First(),
                    Client_ID = _db.Clients.Where(d => d.Client_ID == p.Client_ID).First(),
                    Comment = _db.BillsCategory.Where(d => d.BillCate_ID == p.Cate_Id).Select(f => f.name).FirstOrDefault(),
                    //info
                    Viewed = _db.PLaceInfo.Select(f => f.Img).FirstOrDefault(),
                    active = _db.PLaceInfo.Select(f => f.PlaceName).FirstOrDefault(),
                    status = _db.PLaceInfo.Select(f => f.Number).FirstOrDefault(),

                }).ToList());
                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "aaplication/pdf", "سجل الفواتير.pdf");
            }

            else if (BillCatID != 0 && clientNameID == 0 && UserNameID == 0)


            {
                var modelBillcat = _db.Bills.Where(d => d.date >= from && d.date <= to && d.Cate_Id == BillCatID).ToList();
                rd.SetDataSource(modelBillcat.Select(p => new
                {
                    id = p.Id,
                    Password = from,
                    phone = to,
                    Cate_Id = _db.BillsCategory.Where(d => d.BillCate_ID == BillCatID).Select(f => f.name).FirstOrDefault(),

                    price = modelBillcat.Select(f => f.price).Sum(),
                    cost = modelBillcat.Select(f => f.cost).Sum(),
                    discount = modelBillcat.Select(f => f.discount).Sum(),
                    name = modelBillcat.Select(f => f.price).Sum() - modelBillcat.Select(f => f.discount).Sum(),
                    national_id = (modelBillcat.Select(f => f.price).Sum() - modelBillcat.Select(f => f.discount).Sum()) - modelBillcat.Select(f => f.cost).Sum(),

                    User_ID = _db.Users.Where(d => d.Id == p.User_ID).First(),
                    Client_ID = _db.Clients.Where(d => d.Client_ID == p.Client_ID).First(),
                    Comment = _db.BillsCategory.Where(d => d.BillCate_ID == p.Cate_Id).Select(f => f.name).FirstOrDefault(),
                    //info
                    Viewed = _db.PLaceInfo.Select(f => f.Img).FirstOrDefault(),
                    active = _db.PLaceInfo.Select(f => f.PlaceName).FirstOrDefault(),
                    status = _db.PLaceInfo.Select(f => f.Number).FirstOrDefault(),

                }).ToList());
                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "aaplication/pdf", "سجل الفواتير.pdf");
            }
            else if (BillCatID == 0 && clientNameID != 0 && UserNameID == 0)


            {
                var modelBillcat = _db.Bills.Where(d => d.date >= from && d.date <= to && d.Client_ID == clientNameID).ToList();
                rd.SetDataSource(modelBillcat.Select(p => new
                {
                    id = p.Id,
                    Password = from,
                    phone = to,
                    Cate_Id = _db.BillsCategory.Where(d => d.BillCate_ID == BillCatID).Select(f => f.name).FirstOrDefault(),

                    price = modelBillcat.Select(f => f.price).Sum(),
                    cost = modelBillcat.Select(f => f.cost).Sum(),
                    discount = modelBillcat.Select(f => f.discount).Sum(),
                    name = modelBillcat.Select(f => f.price).Sum() - modelBillcat.Select(f => f.discount).Sum(),
                    national_id = (modelBillcat.Select(f => f.price).Sum() - modelBillcat.Select(f => f.discount).Sum()) - modelBillcat.Select(f => f.cost).Sum(),

                    User_ID = _db.Users.Where(d => d.Id == p.User_ID).First(),
                    Client_ID = _db.Clients.Where(d => d.Client_ID == p.Client_ID).First(),
                    Comment = _db.BillsCategory.Where(d => d.BillCate_ID == p.Cate_Id).Select(f => f.name).FirstOrDefault(),
                    //info
                    Viewed = _db.PLaceInfo.Select(f => f.Img).FirstOrDefault(),
                    active = _db.PLaceInfo.Select(f => f.PlaceName).FirstOrDefault(),
                    status = _db.PLaceInfo.Select(f => f.Number).FirstOrDefault(),

                }).ToList());
                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "aaplication/pdf", "سجل الفواتير.pdf");
            }
            else if (BillCatID == 0 && clientNameID == 0 && UserNameID != 0)


            {
                var modelBillcat = _db.Bills.Where(d => d.date >= from && d.date <= to && d.User_ID == UserNameID).ToList();
                rd.SetDataSource(modelBillcat.Select(p => new
                {
                    id = p.Id,
                    Password = from,
                    phone = to,
                    Cate_Id = _db.BillsCategory.Where(d => d.BillCate_ID == BillCatID).Select(f => f.name).FirstOrDefault(),

                    price = modelBillcat.Select(f => f.price).Sum(),
                    cost = modelBillcat.Select(f => f.cost).Sum(),
                    discount = modelBillcat.Select(f => f.discount).Sum(),
                    name = modelBillcat.Select(f => f.price).Sum() - modelBillcat.Select(f => f.discount).Sum(),
                    national_id = (modelBillcat.Select(f => f.price).Sum() - modelBillcat.Select(f => f.discount).Sum()) - modelBillcat.Select(f => f.cost).Sum(),

                    User_ID = _db.Users.Where(d => d.Id == p.User_ID).First(),
                    Client_ID = _db.Clients.Where(d => d.Client_ID == p.Client_ID).First(),
                    Comment = _db.BillsCategory.Where(d => d.BillCate_ID == p.Cate_Id).Select(f => f.name).FirstOrDefault(),
                    //info
                    Viewed = _db.PLaceInfo.Select(f => f.Img).FirstOrDefault(),
                    active = _db.PLaceInfo.Select(f => f.PlaceName).FirstOrDefault(),
                    status = _db.PLaceInfo.Select(f => f.Number).FirstOrDefault(),

                }).ToList());
                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "aaplication/pdf", "سجل الفواتير.pdf");
            }


            var model = _db.Bills.Where(d => d.date >= from && d.date <= to).ToList();

            rd.SetDataSource(model.Select(p => new
            {

                id = p.Id,
                Password = from,
                phone = to,
                Cate_Id = _db.BillsCategory.Where(d => d.BillCate_ID == BillCatID).Select(f => f.name).FirstOrDefault(),

                price = model.Select(f => f.price).Sum(),
                cost = model.Select(f => f.cost).Sum(),
                discount = model.Select(f => f.discount).Sum(),
                name = model.Select(f => f.price).Sum() - model.Select(f => f.discount).Sum(),
                national_id = (model.Select(f => f.price).Sum() - model.Select(f => f.discount).Sum()) - model.Select(f => f.cost).Sum(),

                User_ID = _db.Users.Where(d => d.Id == p.User_ID).First(),
                Client_ID = _db.Clients.Where(d => d.Client_ID == p.Client_ID).First(),
                Comment = _db.BillsCategory.Where(d => d.BillCate_ID == p.Cate_Id).Select(f => f.name).FirstOrDefault(),
                //info
                Viewed = _db.PLaceInfo.Select(f => f.Img).FirstOrDefault(),
                active = _db.PLaceInfo.Select(f => f.PlaceName).FirstOrDefault(),
                status = _db.PLaceInfo.Select(f => f.Number).FirstOrDefault(),


            }).ToList());
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "aaplication/pdf", "سجل الفواتير.pdf");

        }

        #endregion


        #region  Product report Not compelete



        public ActionResult ProductReort(DateTime from, DateTime to, string BillCat)
        {

            ReportDocument rd = new ReportDocument();

            rd.Load(Path.Combine(Server.MapPath("~/Report/ProductReport.rpt")));
            Stream stream;


            var BillCatID = _db.BillsCategory.Where(p => p.name == BillCat).Select(f => f.BillCate_ID).FirstOrDefault();

            if (BillCatID != 0)
            {
                var model = _db.Bills.Where(d => d.date >= from && d.date <= to && d.Cate_Id == BillCatID).Select(f=>f.Id).ToList();
                List<int> list = new List<int>();
               List<Products> list2 = new List<Products>();
                List<BillsContent> list3 = new List<BillsContent>();

                // Products list2 = new Products();

                foreach (var item in model)
                {
                  var   models = _db.BillsContent.Where(d => d.Bill_ID == item).ToList();

                    foreach (var items in models)
                    {
                        list.Add(items.Product_ID);
                        list3.Add(items);


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

                rd.SetDataSource(list2.Select(p => new
                {
                   
                    Expr2 = from,
                    Expr1 = to,
                    Cate_ID = BillCat,
                    Pro_id = p.name,
                    Quantity = list3.Where(f => f.Product_ID == p.Pro_id).Select(f => f.Quantity).Sum(),

               // Quantity = _db.BillsContent.Where(d => d.Product_ID == p.Pro_id).Select(f => f.Quantity).Sum(),

                }).ToList());
                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "aaplication/pdf", "سجل الفواتير.pdf");

            }
            return View();
        }
        #endregion

        #region Print customer --compelted


        public ActionResult ClientStatistic(DateTime from, DateTime to, int client)
        {

            ReportDocument rd = new ReportDocument();

            rd.Load(Path.Combine(Server.MapPath("~/Report/ClientReportrpt.rpt")));
            Stream stream;

            List<string> list = new List<string>();

            var modelClient = _db.Bills.Where(d => d.date >= from && d.date <= to && d.Client_ID == client).ToList();

            rd.SetDataSource(modelClient.Select(p => new
            {
                nationalID = from,
                Comment = to,
  
                name = _db.Clients.Where(d => d.Client_ID == client).Select(f => f.name).FirstOrDefault(),
 
                 minimum_bills = _db.Bills.Where(d => d.date >= from && d.date <= to && d.Client_ID == client).Count(),
                Price = modelClient.Select(f => f.price).Sum(),
                Payment_amount = _db.Payments.Where(f=>f.client_id == client&&   f.date >= from && f.date <= to).Select(f=>f.Payment_amount).Sum(),
                phone = modelClient.Where(f => f.Client_ID == client && f.date >= from && f.date <= to).Select(f => f.price).Sum() - _db.Payments.Where(f => f.client_id == client && f.date >= from && f.date <= to).Select(f => f.Payment_amount).Sum(),
                //info
                Clients_Type_ID = _db.PLaceInfo.Select(f => f.Img).FirstOrDefault(),
                Active = _db.PLaceInfo.Select(f => f.PlaceName).FirstOrDefault(),
                Address = _db.PLaceInfo.Select(f => f.Number).FirstOrDefault(),

                


            }).ToList());
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "aaplication/pdf", "سجل العملاء .pdf");
        }
        #endregion


        #region ExpensesReport --compelted


        public ActionResult ExpensesReport(DateTime from, DateTime to, string ExpenesCat, string Client)
        {
 

            ReportDocument rd = new ReportDocument();

            rd.Load(Path.Combine(Server.MapPath("~/Report/ExpensesReport.rpt")));
            Stream stream;

            var usrId = _db.Users.Where(d => d.name == Client).Select(f => f.Id).FirstOrDefault();
            var CateExpId = _db.ExpensesType.Where(d => d.name == ExpenesCat).Select(f => f.ExpensesType_ID).FirstOrDefault();

            if (CateExpId != 0 && usrId != 0)
            {
                var model = _db.Expenses.Where(d => d.date >= from && d.date <= to && d.ExpensesType_ID == CateExpId && d.User_ID == usrId).ToList();
                rd.SetDataSource(model.Select(p => new
                {
                     Password = from,
                    phone = to,
                    TypeID =_db.ExpensesType.Where(f=>f.ExpensesType_ID==p.ExpensesType_ID).Select(f=>f.name).FirstOrDefault(),
                    comment = p.comment,
                    //amount = model.Select(f => f.amount).Sum(),
                    amount =p.amount,
                    username=_db.Users.Where(f=>f.Id ==p.User_ID).Select(f=>f.name).FirstOrDefault(),


                    //info
                    PicPath = _db.PLaceInfo.Select(f => f.Img).FirstOrDefault(),
                    active = _db.PLaceInfo.Select(f => f.PlaceName).FirstOrDefault(),
                    name = _db.PLaceInfo.Select(f => f.Number).FirstOrDefault(),



                }).ToList());
                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "aaplication/pdf", "سجل المصروفات.pdf");
            }
            if (CateExpId != 0 && usrId == 0)
            {
                var model = _db.Expenses.Where(d => d.date >= from && d.date <= to && d.ExpensesType_ID == CateExpId).ToList();
                rd.SetDataSource(model.Select(p => new
                {
                    Password = from,
                    phone = to,
                    TypeID = _db.ExpensesType.Where(f => f.ExpensesType_ID == p.ExpensesType_ID).Select(f => f.name).FirstOrDefault(),
                    comment = p.comment,
                    //amount = model.Select(f => f.amount).Sum(),
                    amount = p.amount,
                    username = _db.Users.Where(f => f.Id == p.User_ID).Select(f => f.name).FirstOrDefault(),
                    //info
                    PicPath = _db.PLaceInfo.Select(f => f.Img).FirstOrDefault(),
                    active = _db.PLaceInfo.Select(f => f.PlaceName).FirstOrDefault(),
                    name = _db.PLaceInfo.Select(f => f.Number).FirstOrDefault(),



                }).ToList());
                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "aaplication/pdf", "سجل المصروفات.pdf");
            }
            if (CateExpId == 0 && usrId != 0)
            {
                var model = _db.Expenses.Where(d => d.date >= from && d.date <= to && d.User_ID == usrId).ToList();
                rd.SetDataSource(model.Select(p => new
                {
                    Password = from,
                    phone = to,
                    TypeID = _db.ExpensesType.Where(f => f.ExpensesType_ID == p.ExpensesType_ID).Select(f => f.name).FirstOrDefault(),
                    comment = p.comment,
                    //amount = model.Select(f => f.amount).Sum(),
                    amount = p.amount,
                    username = _db.Users.Where(f => f.Id == p.User_ID).Select(f => f.name).FirstOrDefault(),

                    //info
                    PicPath = _db.PLaceInfo.Select(f => f.Img).FirstOrDefault(),
                    active = _db.PLaceInfo.Select(f => f.PlaceName).FirstOrDefault(),
                    name = _db.PLaceInfo.Select(f => f.Number).FirstOrDefault(),


                }).ToList());
                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "aaplication/pdf", "سجل المصروفات.pdf");
            }
            else
            {


                var model = _db.Expenses.Where(d => d.date >= from && d.date <= to).ToList();
                rd.SetDataSource(model.Select(p => new
                {
                    Password = from,
                    phone = to,
                    TypeID = _db.ExpensesType.Where(f => f.ExpensesType_ID == p.ExpensesType_ID).Select(f => f.name).FirstOrDefault(),
                    comment = p.comment,
                    //amount = model.Select(f => f.amount).Sum(),
                    amount = p.amount,
                    username = _db.Users.Where(f => f.Id == p.User_ID).Select(f => f.name).FirstOrDefault(),
                    //info
                    PicPath = _db.PLaceInfo.Select(f => f.Img).FirstOrDefault(),
                    active = _db.PLaceInfo.Select(f => f.PlaceName).FirstOrDefault(),
                    name = _db.PLaceInfo.Select(f => f.Number).FirstOrDefault(),



                }).ToList());
                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "aaplication/pdf", "سجل المصروفات.pdf");
            }
        }
        #endregion

        #region PaymentStaticstic --Compelted

        public ActionResult PaymentStaticstic (DateTime from, DateTime to, string client)
        {


            ReportDocument rd = new ReportDocument();

            rd.Load(Path.Combine(Server.MapPath("~/Report/PaymentReport.rpt")));
            Stream stream;

            if (client == "")
            {
                var model = _db.Payments.Where(d => d.date >= from && d.date <= to).ToList();
                rd.SetDataSource(model.Select(p => new
                {
                     Payments_ID = from,
                    fatoraID = to,
                    client_id = _db.Clients.Where(a => a.Client_ID == p.client_id).Select(f => f.name).FirstOrDefault(),
                    user_id = _db.Users.Where(a => a.Id == p.user_id).Select(f => f.name).FirstOrDefault(),
                    Payment_amount = p.Payment_amount,
                      date = p.date,
                    //info
                    PicPath = _db.PLaceInfo.Select(f => f.Img).FirstOrDefault(),
                    active = _db.PLaceInfo.Select(f => f.PlaceName).FirstOrDefault(),
                    phone = _db.PLaceInfo.Select(f => f.Number).FirstOrDefault(),


                }).ToList());
                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "aaplication/pdf", "سجل الدفعات الماليه.pdf");

            }
            else
            {
                var model = _db.Payments.Where(d => d.date >= from && d.date <= to && d.client_id.ToString() == client).ToList();
                rd.SetDataSource(model.Select(p => new
                {
                    Payments_ID = from,
                    fatoraID = to,
                    client_id     = _db.Clients.Where(a => a.Client_ID == p.client_id).Select(f => f.name).FirstOrDefault(),
                    user_id  = _db.Users.Where(a => a.Id == p.user_id).Select(f => f.name).FirstOrDefault(),
                    Payment_amount = p.Payment_amount,
                    date = p.date,

                    //info
                    PicPath = _db.PLaceInfo.Select(f => f.Img).FirstOrDefault(),
                    active = _db.PLaceInfo.Select(f => f.PlaceName).FirstOrDefault(),
                    phone = _db.PLaceInfo.Select(f => f.Number).FirstOrDefault(),


                }).ToList());
                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "aaplication/pdf", "سجل الدفعات الماليه.pdf");

            }
        }
        #endregion

    }

}