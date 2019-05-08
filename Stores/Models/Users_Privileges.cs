using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Stores.Models
{
    public class Users_Privileges
    {
        [Key]
        public int id { get; set; }
        [DisplayName("الاشعارات")]

        public bool notifacations { get; set; }
        [DisplayName("فاتوره بيع")]

        public bool salesbill { get; set; }
        [DisplayName("فاتوره شراء")]

        public bool purchasebill { get; set; }
        [DisplayName("فاتوره مرتجع")]

        public bool backbill { get; set; }
        [DisplayName("الاصناف")]

        public bool categories { get; set; }
        [DisplayName("المنتجات")]

        public bool products { get; set; }
        [DisplayName("المصاريف")]

        public bool expenses { get; set; }
        [DisplayName("الدفعات")]

        public bool payment { get; set; }
        [DisplayName("الاحصائيات")]

        public bool statistics { get; set; }

        [DisplayName("انهاء اليوم")]

        public bool endday { get; set; }
        [DisplayName("اعدادات")]

        public bool sitting { get; set; }
        [DisplayName("المستخدمين")]

        public bool users { get; set; }
        [DisplayName("العملاء")]

        public bool clients { get; set; }
        [DisplayName("انواع المصاريف")]

        public bool expenses_type { get; set; }
        public virtual Users User_ID { get; set; }

    }
}