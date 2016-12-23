using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Michaellrg.MOIP.Domain
{
    public class Phone
    {
        public int id { get; set; }
        public string countryCode { get; set; }
        public string areaCode { get; set; }
        public string number { get; set; }

        public virtual ICollection<Customer> customer { get; set; }
    }
}
