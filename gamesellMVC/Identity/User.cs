using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gamesellMVC.Identity
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double Balance { get; set; }
        public int MobileNumber { get; set; }
        public DateTime Dob { get; set; } = DateTime.Now;
        public string profile_pic { get; set; }
        public string back_pic { get; set; }
        public string slider_1 { get; set; }
        public string slider_2 { get; set; }
        public string slider_3 { get; set; }
        public int languageID { get; set; }
        public int currencyID { get; set; }
        public bool premium { get; set; }
        public int seller_rank { get; set; }
        public int seller_score { get; set; }
        public int seller_vote { get; set; }
        public int like { get; set; }
        public int dislike { get; set; }
        public bool IsApproved { get; set; } = true;

        public DateTime Xboxstart { get; set; } = DateTime.Now;
        public DateTime Xboxexpire { get; set; } = DateTime.Now;

        public DateTime xboxdatebuy { get; set; } = DateTime.Now;
        public DateTime xboxdateexpire { get; set; } = DateTime.Now;

    }
}
