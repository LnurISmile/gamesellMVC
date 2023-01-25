using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gamesellMVC.Models
{
    public class BalanceInfoModel
    {
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
