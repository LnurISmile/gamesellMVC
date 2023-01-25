using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.data.Abstract
{
    public interface IDeveloperRepository : IRepository<Developer>
    {
        int GetCount();
        List<Developer> GetSearchResult(string q, int page, int pageSize);
        Developer GetAdDetails(int id);
    }
}
