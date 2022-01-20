using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace awezomestore_administration_ms.Entities
{
    public class PaymentMethods
    {
        public int Payment_method_id { get; set; }
        public string Payment_code { get; set; }
        public DateTime Payment_exp { get; set; }
        public string Payment_sc { get; set; }
        public string Email { get; set; }
    }
}
