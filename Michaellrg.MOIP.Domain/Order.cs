using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Michaellrg.MOIP.Domain
{
    public class Order {
        public string id { get; set; }
        public string ownId { get; set; }
        public string status { get; set; }
        public int  idCustomer { get; set; }

        public virtual Customer customer { get; set; }
    }
}