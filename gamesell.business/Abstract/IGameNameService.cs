using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.business.Abstract
{
    public interface IGameNameService
    {
        GameName GetById(int id);
        List<GameName> GetAll(int page, int pageSize);
        List<GameName> GetAll();
        GameName GetAdDetails(int id);
        List<GameName> GetSearchResult(string q, int page, int pageSize);
        int GetCount();

        void Create(GameName entity);
        void Update(GameName entity);
        void Delete(GameName entity);
    }
}
