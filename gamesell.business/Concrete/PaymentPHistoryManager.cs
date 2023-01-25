using gamesell.business.Abstract;
using gamesell.data.Abstract;
using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.business.Concrete
{
    public class PaymentPHistoryManager : IPaymentPHistoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        public PaymentPHistoryManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Create(PaymentPHistory entity)
        {
            _unitOfWork.PPHs.Create(entity);
            _unitOfWork.Save();
        }

        public void Delete(PaymentPHistory entity)
        {
            _unitOfWork.PPHs.Delete(entity);
            _unitOfWork.Save();
        }

        public PaymentPHistory GetAdDetails(int id)
        {
            return _unitOfWork.PPHs.GetAdDetails(id);
        }

        public List<PaymentPHistory> GetAll(int page, int pageSize)
        {
            return _unitOfWork.PPHs.GetAll(page, pageSize);
        }

        public List<PaymentPHistory> GetAll()
        {
            return _unitOfWork.PPHs.GetAll();
        }

        public PaymentPHistory GetById(int id)
        {
            return _unitOfWork.PPHs.GetById(id);
        }

        public int GetCount()
        {
            return _unitOfWork.PPHs.GetCount();
        }

        public void Update(PaymentPHistory entity)
        {
            _unitOfWork.PPHs.Update(entity);
            _unitOfWork.Save();
        }
    }
}
