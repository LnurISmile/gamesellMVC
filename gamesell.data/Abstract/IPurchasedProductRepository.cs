using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.data.Abstract
{
    public interface IPurchasedProductRepository : IRepository<PurchasedProduct>
    {
        int GetCount();
        List<PurchasedProduct> GetSearchResult(string q, int page, int pageSize);
        PurchasedProduct GetAdDetails(int id);
    }
}
