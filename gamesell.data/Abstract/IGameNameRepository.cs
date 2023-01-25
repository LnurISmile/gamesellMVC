using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.data.Abstract
{
    public interface IGameNameRepository : IRepository<GameName>
    {
        int GetCount();
        List<GameName> GetSearchResult(string q, int page, int pageSize);
        GameName GetAdDetails(int id);
    }
}
