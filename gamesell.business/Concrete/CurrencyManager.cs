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
    public class CurrencyManager : ICurrencyService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CurrencyManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void Create(Currency entity)
        {
            _unitOfWork.Curs.Create(entity);
            _unitOfWork.Save();
        }

        public void Delete(Currency entity)
        {
            _unitOfWork.Curs.Delete(entity);
            _unitOfWork.Save();
        }

        public Currency GetAdDetails(int id)
        {
            return _unitOfWork.Curs.GetAdDetails(id);
        }

        public List<Currency> GetAll(int page, int pageSize)
        {
            return _unitOfWork.Curs.GetAll(page, pageSize);
        }

        public List<Currency> GetAll()
        {
            return _unitOfWork.Curs.GetAll();
        }

        public Currency GetById(int id)
        {
            return _unitOfWork.Curs.GetById(id);
        }

        public int GetCount()
        {
            return _unitOfWork.Curs.GetCount();
        }

        public List<Currency> GetSearchResult(string q, int page, int pageSize)
        {
            return _unitOfWork.Curs.GetSearchResult(q, page, pageSize);
        }

        public void Update(Currency entity)
        {
            _unitOfWork.Curs.Update(entity);
            _unitOfWork.Save();
        }
    }
}
