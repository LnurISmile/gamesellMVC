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
    public class XboxdataManager : IXboxdataService
    {
        private readonly IUnitOfWork _unitOfWork;
        public XboxdataManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Create(Xboxdata entity)
        {
            _unitOfWork.XDs.Create(entity);
            _unitOfWork.Save();
        }

        public void Delete(Xboxdata entity)
        {
            _unitOfWork.XDs.Delete(entity);
            _unitOfWork.Save();
        }

        public Xboxdata GetAdDetails(int id)
        {
            return _unitOfWork.XDs.GetAdDetails(id);
        }

        public List<Xboxdata> GetAll(int page, int pageSize)
        {
            return _unitOfWork.XDs.GetAll(page, pageSize);
        }

        public List<Xboxdata> GetAll()
        {
            return _unitOfWork.XDs.GetAll();
        }

        public Xboxdata GetById(int id)
        {
            return _unitOfWork.XDs.GetById(id);
        }

        public int GetCount()
        {
            return _unitOfWork.XDs.GetCount();
        }

        public void Update(Xboxdata entity)
        {
            _unitOfWork.XDs.Update(entity);
            _unitOfWork.Save();
        }
    }
}
