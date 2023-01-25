using gamesell.data.Abstract;
using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.data.Concrete.EfCore
{
    public class EfCorePurchasedPOGRepository :
        EfCoreGenericRepository<PurchasedPOG>, IPurchasedPOGRepository
    {
        public EfCorePurchasedPOGRepository(PlayPointContext context) : base(context)
        {

        }
        private PlayPointContext PPContext
        {
            get { return context as PlayPointContext; }
        }

        public PurchasedPOG GetAdDetails(int id)
        {
            return PPContext.PPOGs
                .Where(a => a.Id == id)
                .FirstOrDefault();
            throw new NotImplementedException();
        }

        public int GetCount()
        {
            var obj = PPContext.PPOGs
                .Where(i => i.IsApproved)
                .AsQueryable();
            return obj.Count();
        }

        public List<PurchasedPOG> GetSearchResult(string q, int page, int pageSize)
        {
            //var gps = PPContext.PPOGs
            //    .Where(i => i..Contains(q))
            //    .AsQueryable();

            //return gps.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            throw new NotImplementedException();
        }
    }
}
