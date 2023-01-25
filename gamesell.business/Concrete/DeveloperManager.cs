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
    public class DeveloperManager : IDeveloperService
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeveloperManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void Create(Developer entity)
        {
            _unitOfWork.Devs.Create(entity);
            _unitOfWork.Save();
        }

        public void Delete(Developer entity)
        {
            _unitOfWork.Devs.Delete(entity);
            _unitOfWork.Save();
        }

        public Developer GetAdDetails(int id)
        {
            return _unitOfWork.Devs.GetAdDetails(id);
        }

        public List<Developer> GetAll(int page, int pageSize)
        {
            return _unitOfWork.Devs.GetAll(page, pageSize);
        }

        public List<Developer> GetAll()
        {
            return _unitOfWork.Devs.GetAll();
        }

        public Developer GetById(int id)
        {
            return _unitOfWork.Devs.GetById(id);
        }

        public int GetCount()
        {
            return _unitOfWork.Devs.GetCount();
        }

        public List<Developer> GetSearchResult(string q, int page, int pageSize)
        {
            return _unitOfWork.Devs.GetSearchResult(q, page, pageSize);
        }

        public void Update(Developer entity)
        {
            _unitOfWork.Devs.Update(entity);
            _unitOfWork.Save();
        }
    }
}
