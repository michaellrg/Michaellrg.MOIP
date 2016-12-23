using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Michaellrg.MOIP.Domain
{
    public class shippingAddress
    {
        public int id { get; set; }
        public string city { get; set; }
        public string complement { get; set; }
        public string district { get; set; }
        public string street { get; set; }
        public string streetNumber { get; set; }
        public string zipCode { get; set; }
        public string state { get; set; }
        public string country { get; set; }

    }
}
