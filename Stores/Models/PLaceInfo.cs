using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Stores.Models
{
    public class PLaceInfo
    {
        [Key]
        public int id { get; set; }
        [DisplayName("اسم المكان")]

        public string PlaceName { get; set; }
        [DisplayName(" الوصف")]

        public string description { get; set; }
        [DisplayName("رقم التليفون")]


        public string Number { get; set; }
        [DisplayName("الصوره")]

        public string Img { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImgFile { get; set; }
    }
}