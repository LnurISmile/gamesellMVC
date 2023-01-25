using gamesell.entity;
using gamesellMVC.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gamesellMVC.Models
{
    public class CompleteOrderModel
    {
        public User User { get; set; }
        public List<User> Users { get; set; }
        public List<GameName> Gns { get; set; }
        public List<Product_of_Gamer> Pogs { get; set; }
        public List<Product> Pros { get; set; }
    }
}
