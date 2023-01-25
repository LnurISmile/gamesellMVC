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
    public class ActivationCountryManager : IActivationCountryService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ActivationCountryManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void Create(ActivationCountry entity)
        {
            _unitOfWork.ACs.Create(entity);
            _unitOfWork.Save();
        }

        public void Delete(ActivationCountry entity)
        {
            _unitOfWork.ACs.Delete(entity);
            _unitOfWork.Save();
        }

        public ActivationCountry GetAdDetails(int id)
        {
            return _unitOfWork.ACs.GetAdDetails(id);
        }

        public List<ActivationCountry> GetAll(int page, int pageSize)
        {
            return _unitOfWork.ACs.GetAll(page, pageSize);
        }

        public List<ActivationCountry> GetAll()
        {
            return _unitOfWork.ACs.GetAll();
        }

        public ActivationCountry GetById(int id)
        {
            return _unitOfWork.ACs.GetById(id);
        }

        public int GetCount()
        {
            return _unitOfWork.ACs.GetCount();
        }

        public List<ActivationCountry> GetSearchResult(string q, int page, int pageSize)
        {
            return _unitOfWork.ACs.GetSearchResult(q, page, pageSize);
        }

        public void Update(ActivationCountry entity)
        {
            _unitOfWork.ACs.Update(entity);
            _unitOfWork.Save();
        }
    }
}
