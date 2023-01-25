using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.data.Abstract
{
    public interface ICartPRepository : IRepository<CartP>
    {
        CartP GetByUserIdP(string userId);
        void DeleteFromCartP(int cartId, int productId);
    }
}
