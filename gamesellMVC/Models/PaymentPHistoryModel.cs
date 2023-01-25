using gamesell.entity;
using gamesellMVC.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gamesellMVC.Models
{
    public class PaymentPHistoryModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int ProductId { get; set; }
        public double PaymentBalance { get; set; }
        public DateTime BuyDate { get; set; } = DateTime.Now;
    }
    public class PaymentPHistoryListViewModel
    {
        public PageInfo PageInfo { get; set; }
        public List<PaymentPHistory> PPHs { get; set; }
        public List<Product> Pros { get; set; }
        public List<User> Users { get; set; }
    }
    public class PaymentPHistoryaDetailModel
    {
        public PaymentPHistory PPH { get; set; }
    }
}
