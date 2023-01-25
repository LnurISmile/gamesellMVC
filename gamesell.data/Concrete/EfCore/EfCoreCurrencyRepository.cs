using gamesell.data.Abstract;
using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.data.Concrete.EfCore
{
    public class EfCoreCurrencyRepository :
        EfCoreGenericRepository<Currency>, ICurrencyRepository
    {
        public EfCoreCurrencyRepository(PlayPointContext context) : base(context)
        {

        }
        private PlayPointContext PPContext
        {
            get { return context as PlayPointContext; }
        }

        public Currency GetAdDetails(int id)
        {
            return PPContext.Curs
                .Where(a => a.Id == id)
                .FirstOrDefault();
        }

        public int GetCount()
        {
            var obj = PPContext.Curs
                .Where(i => i.IsApproved)
                .AsQueryable();
            return obj.Count();
        }

        public List<Currency> GetSearchResult(string q, int page, int pageSize)
        {
            var gps = PPContext.Curs
                .Where(i => i.CurrencyName.Contains(q))
                .AsQueryable();

            return gps.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }
    }
}
