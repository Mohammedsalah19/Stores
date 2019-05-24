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
    public class StoreHouseController : Controller
    {
        private ProjectContext _db = new ProjectContext();
        private Security s = new Security();



        // GET: StoreHouse
        public ActionResult Index()
        {
            var model = new BillsWithExten();
            model.prodCategoryX = _db.ProductCategory.ToList();
            model.ClientsX = _db.Clients.Where(f => f.Clients_Type_ID == 1).ToList();



            ViewBag.product = new SelectList(_db.Products.ToList(), "Pro_id", "name");
            ViewBag.productCat = new SelectList(_db.ProductCategory.ToList(), "Cate_ID", "name");

            return View(_db.Products.ToList());
        }


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



    }
}