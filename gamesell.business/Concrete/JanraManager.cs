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
    public class JanraManager : IJanraService
    {
        private readonly IUnitOfWork _unitOfWork;
        public JanraManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void Create(Janra entity)
        {
            _unitOfWork.Jans.Create(entity);
            _unitOfWork.Save();
        }

        public void Delete(Janra entity)
        {
            _unitOfWork.Jans.Delete(entity);
            _unitOfWork.Save();
        }

        public Janra GetAdDetails(int id)
        {
            return _unitOfWork.Jans.GetAdDetails(id);
        }

        public List<Janra> GetAll(int page, int pageSize)
        {
            return _unitOfWork.Jans.GetAll(page, pageSize);
        }

        public List<Janra> GetAll()
        {
            return _unitOfWork.Jans.GetAll();
        }

        public Janra GetById(int id)
        {
            return _unitOfWork.Jans.GetById(id);
        }

        public int GetCount()
        {
            return _unitOfWork.Jans.GetCount();
        }

        public List<Janra> GetSearchResult(string q, int page, int pageSize)
        {
            return _unitOfWork.Jans.GetSearchResult(q, page, pageSize);
        }

        public void Update(Janra entity)
        {
            _unitOfWork.Jans.Update(entity);
            _unitOfWork.Save();
        }
    }
}
