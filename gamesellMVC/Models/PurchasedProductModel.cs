using gamesell.entity;
using gamesellMVC.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gamesellMVC.Models
{
    public class PurchasedProductModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int ProductId { get; set; }
        public bool IsApproved { get; set; }
        public bool IsXbox { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;

        public List<Product> Pros { get; set; }
        public List<User> Users { get; set; }
    }
    public class PurchasedProductListViewModel
    {
        public DateTime XboxExpire { get; set; }
        public Xboxdata Xbox { get; set; }
        public PageInfo PageInfo { get; set; }
        public List<PurchasedProduct> PPs { get; set; }
        public List<Product> Pros { get; set; }
        public List<User> Users { get; set; }
        public List<GameName> Gns { get; set; }
        public List<Divice> Divs { get; set; }
        public List<Platform> Plat { get; set; }
    }
}
