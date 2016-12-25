using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Michaellrg.MOIP.Web.Models
{
    public class AddressView
    {
        [Required(ErrorMessage = "City Required")]
        [StringLength(32, MinimumLength = 3, ErrorMessage = "Invalid Character Number")]
        public string City { get; set; }

        [StringLength(42, MinimumLength = 0, ErrorMessage = "Invalid Character Number")]
        public string Complement { get; set; }
        [Required(ErrorMessage = "District Required")]
        [StringLength(45, MinimumLength = 5, ErrorMessage = "Invalid Character Number")]
        public string District { get; set; }
        [Required(ErrorMessage = "State Required")]
        [StringLength(32, MinimumLength = 4, ErrorMessage = "Invalid Character Number")]
        public string State { get; set; }
        [Required(ErrorMessage = "Street Required")]
        [StringLength(45, MinimumLength = 9, ErrorMessage = "Invalid Character Number")]
        public string Street { get; set; }
        [Required(ErrorMessage = "Street Number Required")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Invalid Character Number")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Street Number must be numeric")]
        public string StreetNumber { get; set; }
        
        [Required(ErrorMessage = "ZipCode Required")]
        [StringLength(8, MinimumLength = 8, ErrorMessage = "Invalid Character Number")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Zip Code must be numeric")]
        public string ZipCode { get; set; }
    }
}