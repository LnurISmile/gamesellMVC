using gamesell.data.Abstract;
using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.data.Concrete.EfCore
{
    public class EfCorePaymentPOGHistoryRepository :
        EfCoreGenericRepository<PaymentPOGHistory>, IPaymentPOGHistoryRepository
    {
        public EfCorePaymentPOGHistoryRepository(PlayPointContext context) : base(context)
        {

        }
        private PlayPointContext PPContext
        {
            get { return context as PlayPointContext; }
        }

        public PaymentPOGHistory GetAdDetails(int id)
        {
            return PPContext.PPOGHs
                .Where(a => a.Id == id)
                .FirstOrDefault();
        }

        public int GetCount()
        {
            var obj = PPContext.PPOGHs
                .AsQueryable();
            return obj.Count();
        }
    }
}
