using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.entity
{
    public class BalanceInfo
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string InvoiceNum { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public bool IsApproved { get; set; }
        public bool PayBtnInfo { get; set; }
        public string SE { get; set; }
    }
}
