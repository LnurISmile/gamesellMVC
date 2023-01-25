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
    public class XboxgameManager : IXboxgameService
    {
        private readonly IUnitOfWork _unitOfWork;
        public XboxgameManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Create(Xboxgame entity)
        {
            _unitOfWork.XGs.Create(entity);
            _unitOfWork.Save();
        }

        public void Delete(Xboxgame entity)
        {
            _unitOfWork.XGs.Delete(entity);
            _unitOfWork.Save();
        }

        public List<Xboxgame> GetAll(int page, int pageSize)
        {
            return _unitOfWork.XGs.GetAll(page, pageSize);
        }

        public List<Xboxgame> GetAll()
        {
            return _unitOfWork.XGs.GetAll();
        }

        public List<Xboxgame> GetAllforP(int page, int pageSize)
        {
            return _unitOfWork.XGs.GetAllforP(page, pageSize);
        }

        public Xboxgame GetById(int id)
        {
            return _unitOfWork.XGs.GetById(id);
        }

        public int GetCount()
        {
            return _unitOfWork.XGs.GetCount();
        }

        public void Update(Xboxgame entity)
        {
            _unitOfWork.XGs.Update(entity);
            _unitOfWork.Save();
        }
    }
}
