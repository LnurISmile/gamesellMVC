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
    public class PurchasedProductManager : IPurchasedProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        public PurchasedProductManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void Create(PurchasedProduct entity)
        {
            _unitOfWork.PPs.Create(entity);
            _unitOfWork.Save();
        }

        public void Delete(PurchasedProduct entity)
        {
            _unitOfWork.PPs.Delete(entity);
            _unitOfWork.Save();
        }

        public PurchasedProduct GetAdDetails(int id)
        {
            return _unitOfWork.PPs.GetAdDetails(id);
        }

        public List<PurchasedProduct> GetAll(int page, int pageSize)
        {
            return _unitOfWork.PPs.GetAll(page, pageSize);
        }

        public List<PurchasedProduct> GetAll()
        {
            return _unitOfWork.PPs.GetAll();
        }

        public PurchasedProduct GetById(int id)
        {
            return _unitOfWork.PPs.GetById(id);
        }

        public int GetCount()
        {
            return _unitOfWork.PPs.GetCount();
        }

        public List<PurchasedProduct> GetSearchResult(string q, int page, int pageSize)
        {
            return _unitOfWork.PPs.GetSearchResult(q, page, pageSize);
        }

        public void Update(PurchasedProduct entity)
        {
            _unitOfWork.PPs.Update(entity);
            _unitOfWork.Save();
        }
    }
}
