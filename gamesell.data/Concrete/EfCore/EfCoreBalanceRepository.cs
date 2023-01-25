using gamesell.data.Abstract;
using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.data.Concrete.EfCore
{
    public class EfCoreBalanceRepository :
        EfCoreGenericRepository<BalanceInfo>, IBalanceRepository
    {
        public EfCoreBalanceRepository(PlayPointContext context) : base(context)
        {

        }
        private PlayPointContext PPContext
        {
            get { return context as PlayPointContext; }
        }
        public int GetCount()
        {
            throw new NotImplementedException();
        }

        public List<BalanceInfo> GetSearchResult(string q, int page, int pageSize)
        {
            throw new NotImplementedException();
        }
    }
}
