using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.data.Abstract
{
    public interface IPurchasedPOGRepository : IRepository<PurchasedPOG>
    {
        int GetCount();
        List<PurchasedPOG> GetSearchResult(string q, int page, int pageSize);
        PurchasedPOG GetAdDetails(int id);
    }
}
