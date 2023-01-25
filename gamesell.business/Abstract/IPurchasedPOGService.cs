using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.business.Abstract
{
    public interface IPurchasedPOGService
    {
        PurchasedPOG GetById(int id);
        List<PurchasedPOG> GetAll(int page, int pageSize);
        List<PurchasedPOG> GetAll();
        PurchasedPOG GetAdDetails(int id);
        List<PurchasedPOG> GetSearchResult(string q, int page, int pageSize);
        int GetCount();

        void Create(PurchasedPOG entity);
        void Update(PurchasedPOG entity);
        void Delete(PurchasedPOG entity);
    }
}
