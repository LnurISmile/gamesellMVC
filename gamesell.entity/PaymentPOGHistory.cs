using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.entity
{
    public class PaymentPOGHistory
    {
        [Key]
        public int Id { get; set; }
        public string UserIdSell { get; set; }
        public string UserIdBuy { get; set; }
        public int POGId { get; set; }
        public double PaymentBalance { get; set; }
        public DateTime BuyDate { get; set; } = DateTime.Now;
    }
}
