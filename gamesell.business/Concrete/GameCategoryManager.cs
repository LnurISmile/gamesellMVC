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
    public class GameCategoryManager : IGameCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        public GameCategoryManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void Create(GameCategory entity)
        {
            _unitOfWork.GCs.Create(entity);
            _unitOfWork.Save();
        }

        public void Delete(GameCategory entity)
        {
            _unitOfWork.GCs.Delete(entity);
            _unitOfWork.Save();
        }

        public GameCategory GetAdDetails(int id)
        {
            return _unitOfWork.GCs.GetAdDetails(id);
        }

        public List<GameCategory> GetAll(int page, int pageSize)
        {
            return _unitOfWork.GCs.GetAll(page, pageSize);
        }

        public List<GameCategory> GetAll()
        {
            return _unitOfWork.GCs.GetAll();
        }

        public GameCategory GetById(int id)
        {
            return _unitOfWork.GCs.GetById(id);
        }

        public int GetCount()
        {
            return _unitOfWork.GCs.GetCount();
        }

        public List<GameCategory> GetSearchResult(string q, int page, int pageSize)
        {
            return _unitOfWork.GCs.GetSearchResult(q, page, pageSize);
        }

        public void Update(GameCategory entity)
        {
            _unitOfWork.GCs.Update(entity);
            _unitOfWork.Save();
        }
    }
}
