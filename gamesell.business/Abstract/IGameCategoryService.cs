using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.business.Abstract
{
    public interface IGameCategoryService
    {
        GameCategory GetById(int id);
        List<GameCategory> GetAll(int page, int pageSize);
        List<GameCategory> GetAll();
        GameCategory GetAdDetails(int id);
        List<GameCategory> GetSearchResult(string q, int page, int pageSize);
        int GetCount();

        void Create(GameCategory entity);
        void Update(GameCategory entity);
        void Delete(GameCategory entity);
    }
}
