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
    public class GameNameManager : IGameNameService
    {
        private readonly IUnitOfWork _unitOfWork;
        public GameNameManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void Create(GameName entity)
        {
            _unitOfWork.GNs.Create(entity);
            _unitOfWork.Save();
        }

        public void Delete(GameName entity)
        {
            _unitOfWork.GNs.Delete(entity);
            _unitOfWork.Save();
        }

        public GameName GetAdDetails(int id)
        {
            return _unitOfWork.GNs.GetAdDetails(id);
        }

        public List<GameName> GetAll(int page, int pageSize)
        {
            return _unitOfWork.GNs.GetAll(page, pageSize);
        }

        public List<GameName> GetAll()
        {
            return _unitOfWork.GNs.GetAll();
        }

        public GameName GetById(int id)
        {
            return _unitOfWork.GNs.GetById(id);
        }

        public int GetCount()
        {
            return _unitOfWork.GNs.GetCount();
        }

        public List<GameName> GetSearchResult(string q, int page, int pageSize)
        {
            return _unitOfWork.GNs.GetSearchResult(q, page, pageSize);
        }

        public void Update(GameName entity)
        {
            _unitOfWork.GNs.Update(entity);
            _unitOfWork.Save();
        }
    }
}
