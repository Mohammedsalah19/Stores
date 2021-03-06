﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Stores.Models
{
    public class Users
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("اسم التسجيل")]

        public string username { get; set; }
        [DisplayName("كلمه المرور")]

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("اسم الموظف")]

        public string name { get; set; }
        [DisplayName("الهاتف")]

        public string phone { get; set; }
        [DisplayName("الرقم القومى")]

        public string national_id { get; set; }
        [DisplayName("حاله الحساب")]

        public bool active { get; set; }

        [DisplayName("اسم الطابعه")]

        public string printer_name { get; set; }
        //    public DateTime user_current_date { get; set; }

        //pic path
        [DisplayName("الصورة الشخصية ")]

        public string PicPath { get; set; }

        [NotMapped]
        //pic
        public HttpPostedFileBase Pic { get; set; }





    }
}