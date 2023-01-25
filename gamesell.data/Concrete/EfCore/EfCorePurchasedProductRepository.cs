using gamesell.data.Abstract;
using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.data.Concrete.EfCore
{
    public class EfCorePurchasedProductRepository :
        EfCoreGenericRepository<PurchasedProduct>, IPurchasedProductRepository
    {
        public EfCorePurchasedProductRepository(PlayPointContext context) : base(context)
        {

        }
        private PlayPointContext PPContext
        {
            get { return context as PlayPointContext; }
        }

        public PurchasedProduct GetAdDetails(int id)
        {
            return PPContext.PPs
                .Where(a => a.Id == id)
                .FirstOrDefault();
        }

        public int GetCount()
        {
            var obj = PPContext.PPs
                .Where(i => i.IsApproved)
                .AsQueryable();
            return obj.Count();
        }

        public List<PurchasedProduct> GetSearchResult(string q, int page, int pageSize)
        {
            //var gps = PPContext.PPs
            //    .Where(i => i..Contains(q))
            //    .AsQueryable();

            //return gps.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            throw new NotImplementedException();
        }
    }
}
