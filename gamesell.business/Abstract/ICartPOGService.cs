using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.business.Abstract
{
    public interface ICartPOGService
    {
        void InitializeCartPOG(string userId);
        CartPOG GetCartByUserIdPOG(string userId);
        void AddToCartPOG(string UserId, int pogId);
        void DeleteFromCartPOG(string userId, int pogId);
        List<CartPOG> GetAll();
    }
}
