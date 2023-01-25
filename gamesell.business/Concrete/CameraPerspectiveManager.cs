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
    public class CameraPerspectiveManager : ICameraPerspectiveServices
    {
        private readonly IUnitOfWork _unitOfWork;
        public CameraPerspectiveManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void Create(CameraPerspective entity)
        {
            _unitOfWork.CPs.Create(entity);
            _unitOfWork.Save();
        }

        public void Delete(CameraPerspective entity)
        {
            _unitOfWork.CPs.Delete(entity);
            _unitOfWork.Save();
        }

        public CameraPerspective GetAdDetails(int id)
        {
            return _unitOfWork.CPs.GetAdDetails(id);
        }

        public List<CameraPerspective> GetAll(int page, int pageSize)
        {
            return _unitOfWork.CPs.GetAll(page, pageSize);
        }

        public List<CameraPerspective> GetAll()
        {
            return _unitOfWork.CPs.GetAll();
        }

        public CameraPerspective GetById(int id)
        {
            return _unitOfWork.CPs.GetById(id);
        }

        public int GetCount()
        {
            return _unitOfWork.CPs.GetCount();
        }

        public List<CameraPerspective> GetSearchResult(string q, int page, int pageSize)
        {
            return _unitOfWork.CPs.GetSearchResult(q, page, pageSize);
        }

        public void Update(CameraPerspective entity)
        {
            _unitOfWork.CPs.Update(entity);
            _unitOfWork.Save();
        }
    }
}
