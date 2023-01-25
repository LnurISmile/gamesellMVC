using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace gamesellMVC.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Email_Required")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email_Format")]
        [DisplayName("Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password_Required")]
        [DataType(DataType.Password, ErrorMessage = "Password_Format")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Password_Length")]
        [DisplayName("Password")]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}
