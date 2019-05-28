using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Stores.Models
{
    public class Payments
    {
        [Key]
        public int Payments_ID { get; set; }
        [DisplayName("كميه المدفوعات")]

        public decimal Payment_amount { get; set; }
        [DisplayName("الوقت")]

        public DateTime date { get; set; }

        public virtual int user_id { get; set; }
        public virtual int client_id { get; set; }
        public virtual int fatoraID { get; set; }
    }
}