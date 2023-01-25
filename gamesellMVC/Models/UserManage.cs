using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace gamesellMVC.Models
{
    public class UserManage
    {
        public string UserId { get; set; }

        [Required(ErrorMessage = "Username_Required")]
        [DisplayName("Username")]
        public string NickName { get; set; }

        [Required(ErrorMessage = "Email_Required")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email_Format")]
        [DisplayName("Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "FN_Required")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "FN_Length")]
        [DisplayName("FirstName")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LN_Required")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "LN_Length")]
        [DisplayName("LastName")]
        public string LastName { get; set; }

        [DataType(DataType.PhoneNumber)]
        //[RegularExpression(@"^\d{10}$", ErrorMessage = "MN_Format")]
        [DisplayName("MobileNumber")]
        public int MobileNumber { get; set; }

        [Required(ErrorMessage = "Dob_Required")]
        [DisplayName("Dob")]
        public DateTime Dob { get; set; }

        public string profile_pic { get; set; }
        public string back_pic { get; set; }
        public string slider_1 { get; set; }
        public string slider_2 { get; set; }
        public string slider_3 { get; set; }
        public int languageID { get; set; }
        public int currencyID { get; set; }

        public bool IsApproved { get; set; }
    }
}
