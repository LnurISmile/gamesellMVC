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
    public class GameItemManager : IGameItemService
    {
        private readonly IUnitOfWork _unitOfWork;
        public GameItemManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Create(GameItem entity)
        {
            _unitOfWork.GIs.Create(entity);
            _unitOfWork.Save();
        }

        public void Delete(GameItem entity)
        {
            _unitOfWork.GIs.Delete(entity);
            _unitOfWork.Save();
        }

        public List<GameItem> GetAll(int page, int pageSize)
        {
            return _unitOfWork.GIs.GetAll(page, pageSize);
        }

        public List<GameItem> GetAll()
        {
            return _unitOfWork.GIs.GetAll();
        }

        public GameItem GetById(int id)
        {
            return _unitOfWork.GIs.GetById(id);
        }

        public List<GameItem> GetByIdWithGN(int id)
        {
            return _unitOfWork.GIs.GetByIdWithGN(id);
        }

        public void Update(GameItem entity)
        {
            _unitOfWork.GIs.Update(entity);
            _unitOfWork.Save();
        }
    }
}
