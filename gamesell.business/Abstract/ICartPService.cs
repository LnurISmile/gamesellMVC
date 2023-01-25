using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.business.Abstract
{
    public interface ICartPService
    {
        void InitializeCart(string userId);
        CartP GetCartByUserIdP(string userId);
        void AddToCartP(string UserId, int productId);
        void DeleteFromCartP(string userId, int productId);
        List<CartP> GetAll();
    }
}
