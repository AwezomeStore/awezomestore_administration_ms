using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace awezomestore_administration_ms.Entities
{
    public class Giftcard
    {
        public int Giftcard_id { get; set; }
        public string Giftcard_code { get; set; }
        public double Giftcard_amount { get; set; }
        public DateTime Gifcard_exp { get; set; }
    }
}
