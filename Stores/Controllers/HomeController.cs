using Stores.Models;
using Stores.Models.DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Stores.Controllers
{
    public class HomeController : Controller
    {


        private ProjectContext _db = new ProjectContext();
        private Security s = new Security();

        public ActionResult Index()
        {
            var model = _db.PLaceInfo.FirstOrDefault();
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [HttpPost]
        public ActionResult PlaceInfo(PLaceInfo _info)
        {
            bool res = s.Users();
            if (res == true)
            {
                string filNameS = Path.GetFileNameWithoutExtension(_info.ImgFile.FileName);

                string exten = Path.GetExtension(_info.ImgFile.FileName);
                filNameS = filNameS + DateTime.Now.ToString("yymmssff") + exten;

                //  _info.Img = @"\infoPic\" + filNameS;

                _info.Img = @"\images\" + filNameS;

                /// this for phiscal root which i choose by iis 
                //  filNameS = Path.Combine(Server.MapPath(@"~\infoPic\"), filNameS);

                filNameS = Path.Combine(Server.MapPath(@"~\images\"), filNameS);

                _info.ImgFile.SaveAs(filNameS);
                _db.PLaceInfo.Add(_info);
                _db.SaveChanges();
                return RedirectToAction("PlaceInfo", "Home", _db.PLaceInfo.ToList());
            }
            return RedirectToAction("HavntAccess", "Employee");
        }
        [HttpGet]
        public ActionResult PlaceInfo()
        {

            bool res = s.Users();
            if (res == true)
            {
                return View(_db.PLaceInfo.ToList());
            }
            return RedirectToAction("HavntAccess", "Employee");
        }


        [HttpGet]
        public ActionResult DeleteInfo(int id)

        {

            bool res = s.Users();
            if (res == true)
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                PLaceInfo info = _db.PLaceInfo.Find(id);
                if (info == null)
                {
                    return HttpNotFound();
                }
                return View(info);


            }
            return RedirectToAction("HavntAccess", "Employee");
        }


        [HttpPost, ActionName("DeleteInfo")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfiremDeleteInfo(int id)
        {


            PLaceInfo info = _db.PLaceInfo.Find(id);
            _db.PLaceInfo.Remove(info);
            _db.SaveChanges();
            TempData["InfoDelete"] = "تم حذف المعلومات بنجاح";
            return RedirectToAction("PlaceInfo");


        }


    }
}