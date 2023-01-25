using gamesell.data.Abstract;
using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.data.Concrete.EfCore
{
    public class EfCoreJanraRepository :
        EfCoreGenericRepository<Janra>, IJanraRepository
    {
        public EfCoreJanraRepository(PlayPointContext context) : base(context)
        {

        }
        private PlayPointContext PPContext
        {
            get { return context as PlayPointContext; }
        }

        public Janra GetAdDetails(int id)
        {
            return PPContext.Jans
                .Where(a => a.Id == id)
                .FirstOrDefault();
        }

        public int GetCount()
        {
            var obj = PPContext.Jans
                .Where(i => i.IsApproved)
                .AsQueryable();
            return obj.Count();
        }

        public List<Janra> GetSearchResult(string q, int page, int pageSize)
        {
            var gps = PPContext.Jans
                .Where(i => i.JanraName.Contains(q))
                .AsQueryable();

            return gps.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }
    }
}
