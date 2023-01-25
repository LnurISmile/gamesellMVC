using gamesell.data.Abstract;
using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.data.Concrete.EfCore
{
    public class EfCorePlatformRepository :
        EfCoreGenericRepository<Platform>, IPlatformRepository
    {
        public EfCorePlatformRepository(PlayPointContext context) : base(context)
        {

        }
        private PlayPointContext PPContext
        {
            get { return context as PlayPointContext; }
        }

        public Platform GetAdDetails(int id)
        {
            return PPContext.Plats
                .Where(a => a.Id == id)
                .FirstOrDefault();
        }

        public int GetCount()
        {
            var obj = PPContext.Plats
                .Where(i => i.IsApproved)
                .AsQueryable();
            return obj.Count();
        }

        public List<Platform> GetSearchResult(string q, int page, int pageSize)
        {
            var gps = PPContext.Plats
                .Where(i => i.PlatformName.Contains(q))
                .AsQueryable();

            return gps.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }
    }
}
