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
    public class PlatformManager : IPlatformService
    {
        private readonly IUnitOfWork _unitOfWork;
        public PlatformManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void Create(Platform entity)
        {
            _unitOfWork.Plats.Create(entity);
            _unitOfWork.Save();
        }

        public void Delete(Platform entity)
        {
            _unitOfWork.Plats.Delete(entity);
            _unitOfWork.Save();
        }

        public Platform GetAdDetails(int id)
        {
            return _unitOfWork.Plats.GetAdDetails(id);
        }

        public List<Platform> GetAll(int page, int pageSize)
        {
            return _unitOfWork.Plats.GetAll(page, pageSize);
        }

        public List<Platform> GetAll()
        {
            return _unitOfWork.Plats.GetAll();
        }

        public Platform GetById(int id)
        {
            return _unitOfWork.Plats.GetById(id);
        }

        public int GetCount()
        {
            return _unitOfWork.Plats.GetCount();
        }

        public List<Platform> GetSearchResult(string q, int page, int pageSize)
        {
            return _unitOfWork.Plats.GetSearchResult(q, page, pageSize);
        }

        public void Update(Platform entity)
        {
            _unitOfWork.Plats.Update(entity);
            _unitOfWork.Save();
        }
    }
}
