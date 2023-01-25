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
    public class BalanceManager : IBalanceService
    {
        private readonly IUnitOfWork _unitOfWork;
        public BalanceManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void Create(BalanceInfo entity)
        {
            _unitOfWork.Bals.Create(entity);
            _unitOfWork.Save();
        }

        public void Delete(BalanceInfo entity)
        {
            _unitOfWork.Bals.Delete(entity);
            _unitOfWork.Save();
        }

        public List<BalanceInfo> GetAll(int page, int pageSize)
        {
            return _unitOfWork.Bals.GetAll(page, pageSize);
        }

        public List<BalanceInfo> GetAll()
        {
            return _unitOfWork.Bals.GetAll();
        }

        public BalanceInfo GetById(int id)
        {
            return _unitOfWork.Bals.GetById(id);
        }

        public int GetCount()
        {
            return _unitOfWork.Bals.GetCount();
        }

        public List<BalanceInfo> GetSearchResult(string q, int page, int pageSize)
        {
            return _unitOfWork.Bals.GetSearchResult(q, page, pageSize);
        }

        public void Update(BalanceInfo entity)
        {
            _unitOfWork.Bals.Update(entity);
            _unitOfWork.Save();
        }
    }
}
