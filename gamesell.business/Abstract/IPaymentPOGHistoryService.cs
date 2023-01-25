using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.business.Abstract
{
    public interface IPaymentPOGHistoryService
    {
        PaymentPOGHistory GetById(int id);
        List<PaymentPOGHistory> GetAll(int page, int pageSize);
        List<PaymentPOGHistory> GetAll();
        PaymentPOGHistory GetAdDetails(int id);
        int GetCount();

        void Create(PaymentPOGHistory entity);
        void Update(PaymentPOGHistory entity);
        void Delete(PaymentPOGHistory entity);
    }
}
