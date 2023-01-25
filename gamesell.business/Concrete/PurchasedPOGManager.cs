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
    public class PurchasedPOGManager : IPurchasedPOGService
    {
        private readonly IUnitOfWork _unitOfWork;
        public PurchasedPOGManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Create(PurchasedPOG entity)
        {
            _unitOfWork.PPOGs.Create(entity);
            _unitOfWork.Save();
        }

        public void Delete(PurchasedPOG entity)
        {
            _unitOfWork.PPOGs.Delete(entity);
            _unitOfWork.Save();
        }

        public PurchasedPOG GetAdDetails(int id)
        {
            return _unitOfWork.PPOGs.GetAdDetails(id);
        }

        public List<PurchasedPOG> GetAll(int page, int pageSize)
        {
            return _unitOfWork.PPOGs.GetAll(page, pageSize);
        }

        public List<PurchasedPOG> GetAll()
        {
            return _unitOfWork.PPOGs.GetAll();
        }

        public PurchasedPOG GetById(int id)
        {
            return _unitOfWork.PPOGs.GetById(id);
        }

        public int GetCount()
        {
            return _unitOfWork.PPOGs.GetCount();
        }

        public List<PurchasedPOG> GetSearchResult(string q, int page, int pageSize)
        {
            return _unitOfWork.PPOGs.GetSearchResult(q, page, pageSize);
        }

        public void Update(PurchasedPOG entity)
        {
            _unitOfWork.PPOGs.Update(entity);
            _unitOfWork.Save();
        }
    }
}
