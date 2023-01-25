using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.entity
{
    public class CartP
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public List<CartItemP> CartItemps { get; set; }
    }
}
