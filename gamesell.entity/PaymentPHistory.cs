using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.entity
{
    public class PaymentPHistory
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public int ProductId { get; set; }
        public double PaymentBalance { get; set; }
        public bool IsXbox { get; set; }
        public DateTime BuyDate { get; set; } = DateTime.Now;
    }
}
