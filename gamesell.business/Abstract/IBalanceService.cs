using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.business.Abstract
{
    public interface IBalanceService
    {
        BalanceInfo GetById(int id);
        List<BalanceInfo> GetAll(int page, int pageSize);
        List<BalanceInfo> GetAll();
        List<BalanceInfo> GetSearchResult(string q, int page, int pageSize);
        int GetCount();
        void Create(BalanceInfo entity);
        void Update(BalanceInfo entity);
        void Delete(BalanceInfo entity);
    }
}
