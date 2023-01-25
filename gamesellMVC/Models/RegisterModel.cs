using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace gamesellMVC.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Firstname_Required")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Firstname_Length")]
        [DisplayName("Firstname")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Lastname_Required")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Lastname_Length")]
        [DisplayName("Lastname")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Username_Required")]
        [DisplayName("Lastname")]
        public string NickName { get; set; }

        [Required(ErrorMessage = "Email_Required")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email_Format")]
        [DisplayName("Email")]
        public string Email { get; set; }

        [DisplayName("MobileNumber")]
        public int MobileNumber { get; set; }

        [Required(ErrorMessage = "Password_Required")]
        [DataType(DataType.Password, ErrorMessage = "Password_Format")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Password_Length")]
        [DisplayName("Password")]
        public string Password { get; set; }

        //[Required(ErrorMessage = "Şifrə təkrarını daxil edin")]
        //[DataType(DataType.Password)]
        //[Compare("Password", ErrorMessage = "Şifrələr eyni deyil")]
        //[StringLength(20, MinimumLength = 6, ErrorMessage = "Şifrə 6-20 simvol aralığında olmalıdır")]
        [DisplayName("RePassword")]
        public string RePassword { get; set; }

        [Required(ErrorMessage = "Dob_Required")]
        [DisplayName("Dob")]
        public DateTime Dob { get; set; }

        public bool EmailConfirmed { get; set; }
        public bool IsApproved { get; set; }
    }
}
