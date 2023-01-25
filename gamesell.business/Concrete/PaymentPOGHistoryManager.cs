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
    public class PaymentPOGHistoryManager : IPaymentPOGHistoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        public PaymentPOGHistoryManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Create(PaymentPOGHistory entity)
        {
            _unitOfWork.PPOGHs.Create(entity);
            _unitOfWork.Save();
        }

        public void Delete(PaymentPOGHistory entity)
        {
            _unitOfWork.PPOGHs.Delete(entity);
            _unitOfWork.Save();
        }

        public PaymentPOGHistory GetAdDetails(int id)
        {
            return _unitOfWork.PPOGHs.GetAdDetails(id);
        }

        public List<PaymentPOGHistory> GetAll(int page, int pageSize)
        {
            return _unitOfWork.PPOGHs.GetAll(page, pageSize);
        }

        public List<PaymentPOGHistory> GetAll()
        {
            return _unitOfWork.PPOGHs.GetAll();
        }

        public PaymentPOGHistory GetById(int id)
        {
            return _unitOfWork.PPOGHs.GetById(id);
        }

        public int GetCount()
        {
            return _unitOfWork.PPOGHs.GetCount();
        }

        public void Update(PaymentPOGHistory entity)
        {
            _unitOfWork.PPOGHs.Update(entity);
            _unitOfWork.Save();
        }
    }
}
