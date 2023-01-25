using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.data.Abstract
{
    public interface ICurrencyRepository : IRepository<Currency>
    {
        int GetCount();
        List<Currency> GetSearchResult(string q, int page, int pageSize);
        Currency GetAdDetails(int id);
    }
}
