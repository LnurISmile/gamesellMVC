using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.data.Abstract
{
    public interface IPaymentPHistoryRepository : IRepository<PaymentPHistory>
    {
        int GetCount();
        PaymentPHistory GetAdDetails(int id);

    }
}
