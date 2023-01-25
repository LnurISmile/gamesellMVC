using gamesell.entity;
using gamesellMVC.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace gamesellMVC.Models
{
    public class AddBalanceModel
    {
        public User User { get; set; }
        public string public_key { get; set; }
        public string secrect_key { get; set; }

        [Required]
        //[Range(1, 1000, ErrorMessage = "The balance should be in the range of 1-1000")]
        public string amount { get; set; }
        public string currency { get; set; }
        public string language { get; set; }
        public int order_id { get; set; }
        public string data { get; set; }
        public string signature { get; set; }
        public string description { get; set; }
        public BalanceInfo BIM { get; set; }
    }
}
