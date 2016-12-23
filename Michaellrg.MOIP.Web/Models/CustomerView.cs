using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Moip.Net.V2.Model;
using System.ComponentModel.DataAnnotations;

namespace Michaellrg.MOIP.Web.Models
{
    public class CustomerView
    {
        [Required(ErrorMessage = "Birth Required")]
        [DataType (DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public string BirthDate { get; set; }

        [Required(ErrorMessage = "Email Required")]
        [DataType(DataType.EmailAddress)]
        [StringLength(45, MinimumLength = 9, ErrorMessage = "Invalid Character Number")]
        public string Email { get; set; }

        [Required (ErrorMessage ="FullName Required")]
        [StringLength(90, MinimumLength = 9, ErrorMessage = "Invalid Character Number")]
        public string Fullname { get; set; }



        // Phone

        [Required(ErrorMessage = "Invalid Area Code")]
        [MaxLength(2, ErrorMessage = "Invalid Character Number")]
        [MinLength(2, ErrorMessage = "Invalid Character Number")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Area Code must be numeric")]
        public string AreaCode { get; set; }

        [Required(ErrorMessage = "Invalid Number")]
        [MaxLength(9, ErrorMessage = "Invalid Character Number")]
        [MinLength(8, ErrorMessage = "Invalid Character Number")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Phone Number must be numeric")]
        public string NumberPhone { get; set; }



        //CPF
        [Required(ErrorMessage = "Invalid Document")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Invalid Character Number")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "CPF must be numeric")]
        public string Number { get; set; }

    }
}