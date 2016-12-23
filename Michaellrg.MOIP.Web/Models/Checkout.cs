using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Michaellrg.MOIP.Web.Models
{
    public class Checkout:Item
    {
       
        public int idItem { get; set; }
        public int quantity { get; set; }

       
    }
}