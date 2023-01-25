using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.entity
{
    public class CartPOG
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public List<CartItemPOG> CartItempogs { get; set; }
    }
}
