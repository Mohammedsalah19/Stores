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
         //   model.productX = _db.Products.ToList();
 

            ViewBag.product = new SelectList(_db.Products.ToList(), "Pro_id", "name");
            ViewBag.productCat = new SelectList(_db.ProductCategory.ToList(), "Cate_ID", "name");
            return View(model);

        }
        #region retuern cat


        public JsonResult GetcategoryPro(string Pro_id)
        {

            List<Products> cat = new List<Products>();


            var s = _db.ProductCategory.Where(ss => ss.name == Pro_id).FirstOrDefault();

            var obj = _db.Products.Where(p => p.Cate_ID==s.Cate_ID).ToList();

            if (obj !=null && obj.Count()>0)
            {
                foreach (var item in obj)
                {
                    Products model= new Products();
                    model.Pro_id = item.Pro_id;
                    model.name = item.name;
                     cat.Add(model);
                }
            }

            return Json(cat, JsonRequestBehavior.AllowGet);
        }
        #endregion


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

            return Json(result,JsonRequestBehavior.AllowGet);
        }



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
    }
}