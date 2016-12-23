using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Michaellrg.MOIP.Domain
{

    public class Customer
    {
        public string id { get; set; }
        public string ownId { get; set; }
        public string fullname { get; set; }
        public string email { get; set; }
        public string birthDate { get; set; }
        public  int idPhone { get; set; }
        public int idShippingAddress { get; set; }
        public int idTaxDocument { get; set; }


        public virtual Phone phone { get; set; }
        public virtual shippingAddress shippingAddress { get; set; }
        public virtual Taxdocument taxDocument { get; set; }


    }
}
