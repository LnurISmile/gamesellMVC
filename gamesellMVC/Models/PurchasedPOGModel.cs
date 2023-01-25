using gamesell.entity;
using gamesellMVC.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gamesellMVC.Models
{
    public class PurchasedPOGModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int POGId { get; set; }
        public bool IsApproved { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;

        public List<Product_of_Gamer> POGs { get; set; }
        public List<GameName> GNs { get; set; }
        public List<User> Users { get; set; }
    }
    public class PurchasedPOGListViewModel
    {
        public PageInfo PageInfo { get; set; }
        public List<PurchasedPOG> PPOGs { get; set; }
        public List<Product_of_Gamer> POGs { get; set; }
        public List<User> Users { get; set; }
        public List<GameName> Gns { get; set; }
        public List<Divice> Divs { get; set; }
    }
}
