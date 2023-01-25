using gamesell.data.Abstract;
using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.data.Concrete.EfCore
{
    public class EfCoreActivationCountryRepository :
        EfCoreGenericRepository<ActivationCountry>, IActivationCountryRepository
    {
        public EfCoreActivationCountryRepository(PlayPointContext context) : base(context)
        {

        }
        private PlayPointContext PPContext
        {
            get { return context as PlayPointContext; }
        }

        public ActivationCountry GetAdDetails(int id)
        {
            return PPContext.ACs
                .Where(a => a.Id == id)
                .FirstOrDefault();
        }

        public int GetCount()
        {
            var obj = PPContext.ACs
                .Where(i => i.IsApproved)
                .AsQueryable();
            return obj.Count();
        }

        public List<ActivationCountry> GetSearchResult(string q, int page, int pageSize)
        {
            var gps = PPContext.ACs
                .Where(i => i.ActivationCountryName.Contains(q))
                .AsQueryable();

            return gps.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }
    }
}
