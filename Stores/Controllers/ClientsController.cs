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
    public class ClientsController : Controller
    {


        private ProjectContext _db = new ProjectContext();
        private Security s = new Security();

        // GET: Clients
        public ActionResult Index()

        {
            bool res = s.Clients();
            if (res == true)
            {
                var model = new ClientWithExten();

                model.ClientsX = _db.Clients.ToList();
                model.Clients_Type = _db.Clients_Type.ToList();
                return View(model);
            }
            return RedirectToAction("HavntAccess", "Employee");


        }



        #region add client

        public JsonResult SaveClientData(Clients model, string Clients_Type_ID)
        {

            var clintTypeId = _db.Clients_Type.Where(p => p.name == Clients_Type_ID).Select(f => f.Clients_Type_id).FirstOrDefault();
            bool result = true;
            try
            {
                model.Clients_Type_ID = clintTypeId; ;

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



        #region  edite record

        public JsonResult GetClientById(int Client_ID)
        {
            Clients model = _db.Clients.Where(x => x.Client_ID == Client_ID).SingleOrDefault();
            string value = string.Empty;
            value = JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(value, JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region save after edit 


        //public ActionResult Save(Clients model , int Client_IDX,string Clients_Type_ID,string nameX, string phoneX, string nationalIDX, string AddressX, string CommentX, bool ActiveX, decimal minimum_bills)
        //{
        //    var result = false;
        //    var clintTypeId = _db.Clients_Type.Where(p => p.name == Clients_Type_ID).Select(f => f.Clients_Type_id).FirstOrDefault();


        //        Clients pro = _db.Clients.SingleOrDefault(x => x.Client_ID == Client_IDX);
        //        pro.name = nameX;
        //        pro.Address =AddressX;
        //        pro.Clients_Type_ID = clintTypeId;
        //        pro.Comment = CommentX;
        //        pro.minimum_bills = minimum_bills;
        //        pro.nationalID = nationalIDX; ;
        //        pro.phone = phoneX; ;
        //        pro.Active = ActiveX;

        //         _db.SaveChanges();

        //        result = true;




        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}



        [HttpGet]
        public ActionResult EditClient(int? id)
        {
            bool res = s.Clients();
            if (res == true)
            {

                ViewBag.Clients_Type_id = new SelectList(_db.Clients_Type.ToList(), "Clients_Type_id", "name");

                if (id == null)
                {
                    return HttpNotFound();
                }
                var model = _db.Clients.Find(id);
                if (model == null)
                {
                    return HttpNotFound();

                }



                return View(model);

            }
            return RedirectToAction("HavntAccess", "Employee");

        }


        [HttpPost]

        public ActionResult EditClient(Clients _client)
        {
            bool res = s.Clients();
            if (res == true)
            {
                ViewBag.Clients_Type_id = new SelectList(_db.Clients_Type.ToList(), "Clients_Type_id", "name");

                _db.Entry(_client).State = EntityState.Modified;

                _db.SaveChanges();


                return RedirectToAction("index");
            }
            return RedirectToAction("HavntAccess", "Employee");

        }

        #endregion
    }
}