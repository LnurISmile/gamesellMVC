using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.data.Abstract
{
    public interface IJanraRepository : IRepository<Janra>
    {
        int GetCount();
        List<Janra> GetSearchResult(string q, int page, int pageSize);
        Janra GetAdDetails(int id);
    }
}
