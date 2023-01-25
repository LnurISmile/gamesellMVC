using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gamesellMVC.Models
{
    public class RegisterAdminModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int MobileNumber { get; set; }
        public DateTime Dob { get; set; } = DateTime.Now;
        public bool IsApproved { get; set; }
        public string Password { get; set; }
        public string profile_pic { get; set; }
        public string back_pic { get; set; }
        public string slider_1 { get; set; }
        public string slider_2 { get; set; }
        public string slider_3 { get; set; }
        public int languageID { get; set; }
        public int currencyID { get; set; }
        public bool premium { get; set; }
        public int seller_rank { get; set; }
        public int like { get; set; }
        public int dislike { get; set; }

        public IEnumerable<string> SelectedRoles { get; set; }
        public List<Language> Langs { get; set; }
        public List<Currency> Curs { get; set; }
    }
}
