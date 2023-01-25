using gamesell.entity;
using gamesellMVC.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gamesellMVC.Models
{
    public class PaymentPOGHistoryModel
    {
        public int Id { get; set; }
        public string UserIdSell { get; set; }
        public string UserIdBuy { get; set; }
        public int POGId { get; set; }
        public double PaymentBalance { get; set; }
        public DateTime BuyDate { get; set; } = DateTime.Now;
    }
    public class PaymentPOGHistoryListViewModel
    {
        public PageInfo PageInfo { get; set; }
        public List<PaymentPOGHistory> PPOGHs { get; set; }
        public List<Product_of_Gamer> POGs { get; set; }
        public List<GameName> GNs { get; set; }
        public List<User> Users { get; set; }
    }
    public class PaymentPOGHistoryDetailModel
    {
        public PaymentPOGHistory PPOGH { get; set; }
    }
}
