using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.entity
{
    public class CartItemPOG
    {
        public int Id { get; set; }
        public int POGId { get; set; }
        public Product_of_Gamer POG { get; set; }
        public int CartId { get; set; }
        public CartPOG Cartpog { get; set; }
    }
}
