using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.business.Abstract
{
    public interface IGameItemService
    {
        GameItem GetById(int id);
        List<GameItem> GetAll(int page, int pageSize);
        List<GameItem> GetAll();
        List<GameItem> GetByIdWithGN(int id);

        void Create(GameItem entity);
        void Update(GameItem entity);
        void Delete(GameItem entity);
    }
}
