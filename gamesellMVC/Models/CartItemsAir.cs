using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gamesellMVC.Models
{
    public class CartItemsAir
    {
        public int Id { get; set; }
        public string PName { get; set; }
        public string PImg { get; set; }
        public double PPrice { get; set; }
        public string Cur { get; set; }
    }
}
