using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gamesellMVC.Models
{
    public class CartPModel
    {
        public int CartPId { get; set; }
        public List<CartItemPModel> CartItemsP { get; set; }
        public List<Platform> Plats { get; set; }
        public int Cur { get; set; }
        public List<Currency> Curs { get; set; }

        public double TotalPriceP()
        {
            return CartItemsP.Sum(i => i.Const);
        }
    }
    public class CartPOGModel
    {
        public int CartPogId { get; set; }
        public List<CartItemPOGModel> CartItemsPog { get; set; }
        public List<Platform> Plats { get; set; }
        public List<GameName> GNs { get; set; }

        public double TotalPricePog()
        {
            return CartItemsPog.Sum(i => i.Price);
        }
    }

    public class CartItemPModel
    {
        public int Cur { get; set; }
        public List<Currency> Curs { get; set; }
        public int CartItemId { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Platform { get; set; }
        public string Img { get; set; }
        public bool IsProduct { get; set; }
        public int Discount { get; set; }
        public double Const { get; set; }
    }

    public class CartItemPOGModel
    {
        public int CartItemId { get; set; }
        public int POGId { get; set; }
        public int Name { get; set; }
        public double Price { get; set; }
        public int Platform { get; set; }
        public string Img { get; set; }
        public bool IsPOG { get; set; }
    }
}
