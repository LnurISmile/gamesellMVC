using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.data.Abstract
{
    public interface ICartPOGRepository : IRepository<CartPOG>
    {
        CartPOG GetByUserIdPOG(string userId);
        void DeleteFromCartPOG(int cartId, int pogId);
    }
}
