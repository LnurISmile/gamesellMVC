using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.business.Abstract
{
    public interface IPaymentPHistoryService
    {
        PaymentPHistory GetById(int id);
        List<PaymentPHistory> GetAll(int page, int pageSize);
        List<PaymentPHistory> GetAll();
        PaymentPHistory GetAdDetails(int id);
        int GetCount();

        void Create(PaymentPHistory entity);
        void Update(PaymentPHistory entity);
        void Delete(PaymentPHistory entity);
    }
}
