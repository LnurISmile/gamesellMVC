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
    public class DiviceManager : IDiviceService
    {
        private readonly IUnitOfWork _unitOfWork;
        public DiviceManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void Create(Divice entity)
        {
            _unitOfWork.Divs.Create(entity);
            _unitOfWork.Save();
        }

        public void Delete(Divice entity)
        {
            _unitOfWork.Divs.Delete(entity);
            _unitOfWork.Save();
        }

        public Divice GetAdDetails(int id)
        {
            return _unitOfWork.Divs.GetAdDetails(id);
        }

        public List<Divice> GetAll(int page, int pageSize)
        {
            return _unitOfWork.Divs.GetAll(page, pageSize);
        }

        public List<Divice> GetAll()
        {
            return _unitOfWork.Divs.GetAll();
        }

        public Divice GetById(int id)
        {
            return _unitOfWork.Divs.GetById(id);
        }

        public int GetCount()
        {
            return _unitOfWork.Divs.GetCount();
        }

        public List<Divice> GetSearchResult(string q, int page, int pageSize)
        {
            return _unitOfWork.Divs.GetSearchResult(q, page, pageSize);
        }

        public void Update(Divice entity)
        {
            _unitOfWork.Divs.Update(entity);
            _unitOfWork.Save();
        }
    }
}
