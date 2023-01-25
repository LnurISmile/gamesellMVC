using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace gamesellMVC.Models
{
    public class RegisterModelF
    {
        [Required(ErrorMessage = "NickName_Required")]
        [DisplayName("NickName")]
        public string NickName { get; set; }

        [Required(ErrorMessage = "Email_Required")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email_Format")]
        [DisplayName("Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password_Required")]
        //[DataType(DataType.Password, ErrorMessage = "Password_Format")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Password_Length")]
        [DisplayName("Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Dob_Required")]
        [DisplayName("Dob")]
        public DateTime Dob { get; set; }

        [Range(typeof(bool), "true", "true", ErrorMessage = "Pol_Required")]
        [DisplayName("Pol")]
        public bool Policy { get; set; }

        public bool EmailConfirmed { get; set; } = false;
        public bool IsApproved { get; set; } = false;
    }
}
