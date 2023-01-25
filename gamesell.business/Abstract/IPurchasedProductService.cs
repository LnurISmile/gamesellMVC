using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.business.Abstract
{
    public interface IPurchasedProductService
    {
        PurchasedProduct GetById(int id);
        List<PurchasedProduct> GetAll(int page, int pageSize);
        List<PurchasedProduct> GetAll();
        PurchasedProduct GetAdDetails(int id);
        List<PurchasedProduct> GetSearchResult(string q, int page, int pageSize);
        int GetCount();

        void Create(PurchasedProduct entity);
        void Update(PurchasedProduct entity);
        void Delete(PurchasedProduct entity);
    }
}
