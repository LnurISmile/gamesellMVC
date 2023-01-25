using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.data.Abstract
{
    public interface IGameCategoryRepository : IRepository<GameCategory>
    {
        int GetCount();
        List<GameCategory> GetSearchResult(string q, int page, int pageSize);
        GameCategory GetAdDetails(int id);
    }
}
