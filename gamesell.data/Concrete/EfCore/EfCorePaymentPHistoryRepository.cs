using gamesell.data.Abstract;
using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.data.Concrete.EfCore
{
    public class EfCorePaymentPHistoryRepository :
        EfCoreGenericRepository<PaymentPHistory>, IPaymentPHistoryRepository
    {
        public EfCorePaymentPHistoryRepository(PlayPointContext context) : base(context)
        {

        }
        private PlayPointContext PPContext
        {
            get { return context as PlayPointContext; }
        }

        public PaymentPHistory GetAdDetails(int id)
        {
            return PPContext.PPHs
                .Where(a => a.Id == id)
                .FirstOrDefault();
        }

        public int GetCount()
        {
            var obj = PPContext.PPHs
                .AsQueryable();
            return obj.Count();
        }
    }
}
