using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.data.Abstract
{
    public interface IDiviceRepository : IRepository<Divice>
    {
        int GetCount();
        List<Divice> GetSearchResult(string q, int page, int pageSize);
        Divice GetAdDetails(int id);
    }
}
