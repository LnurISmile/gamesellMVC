using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.data.Abstract
{
    public interface IBalanceRepository : IRepository<BalanceInfo>
    {
        int GetCount();
        List<BalanceInfo> GetSearchResult(string q, int page, int pageSize);
    }
}
