using gamesell.data.Abstract;
using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.data.Concrete.EfCore
{
    public class EfCoreDiviceRepository :
        EfCoreGenericRepository<Divice>, IDiviceRepository
    {
        public EfCoreDiviceRepository(PlayPointContext context) : base(context)
        {

        }
        private PlayPointContext PPContext
        {
            get { return context as PlayPointContext; }
        }

        public Divice GetAdDetails(int id)
        {
            return PPContext.Divs
                .Where(a => a.Id == id)
                .FirstOrDefault();
        }

        public int GetCount()
        {
            var obj = PPContext.Divs
                .Where(i => i.IsApproved)
                .AsQueryable();
            return obj.Count();
        }

        public List<Divice> GetSearchResult(string q, int page, int pageSize)
        {
            var gps = PPContext.Divs
                .Where(i => i.DiviceName.Contains(q))
                .AsQueryable();

            return gps.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }
    }
}
