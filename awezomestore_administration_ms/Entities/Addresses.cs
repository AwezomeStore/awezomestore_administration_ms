using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace awezomestore_administration_ms.Entities
{
    public class Addresses
    {
        public int Address_id { get; set; }
        public string Address_Name { get; set; }
        public int Zipcode { get; set; }
        public string City { get; set; }
        public int Phone { get; set; }
        public string Departamento { get; set; }
        public string Address_details { get; set; }
    }
}
