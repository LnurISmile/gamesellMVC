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
    public class EfCoreCartPRepository :
        EfCoreGenericRepository<CartP>, ICartPRepository
    {
        public EfCoreCartPRepository(PlayPointContext context) : base(context)
        {

        }
        private PlayPointContext PPContext
        {
            get { return context as PlayPointContext; }
        }

        public void DeleteFromCartP(int cartId, int productId)
        {
            var cmd = @"delete from CIs where CartId=@p0 and ProductId=@p1";
            PPContext.Database.ExecuteSqlRaw(cmd, cartId, productId);
        }

        public CartP GetByUserIdP(string userId)
        {
            return PPContext.Cartps
                .Include(i => i.CartItemps)
                .ThenInclude(i => i.Product)
                .FirstOrDefault(i => i.UserId == userId);
        }

        public override void Update(CartP entity)
        {
            PPContext.Cartps.Update(entity);
        }
    }
}
