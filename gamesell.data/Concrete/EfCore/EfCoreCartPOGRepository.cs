using gamesell.data.Abstract;
using gamesell.entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.data.Concrete.EfCore
{
    public class EfCoreCartPOGRepository :
        EfCoreGenericRepository<CartPOG>, ICartPOGRepository
    {
        public EfCoreCartPOGRepository(PlayPointContext context) : base(context)
        {

        }
        private PlayPointContext PPContext
        {
            get { return context as PlayPointContext; }
        }

        public void DeleteFromCartPOG(int cartId, int pogId)
        {
            var cmd = @"delete from CIpogs where CartId=@p0 and POGId=@p1";
            PPContext.Database.ExecuteSqlRaw(cmd, cartId, pogId);
            throw new NotImplementedException();
        }

        public CartPOG GetByUserIdPOG(string userId)
        {
            return PPContext.Cartpogs
                .Include(i => i.CartItempogs)
                .ThenInclude(i => i.POG)
                .FirstOrDefault(i => i.UserId == userId);
        }

        public override void Update(CartPOG entity)
        {
            PPContext.Cartpogs.Update(entity);
        }
    }
}
