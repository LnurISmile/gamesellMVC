using gamesell.data.Abstract;
using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.data.Concrete.EfCore
{
    public class EfCoreGameNameRepository :
        EfCoreGenericRepository<GameName>, IGameNameRepository
    {
        public EfCoreGameNameRepository(PlayPointContext context) : base(context)
        {

        }
        private PlayPointContext PPContext
        {
            get { return context as PlayPointContext; }
        }

        public GameName GetAdDetails(int id)
        {
            return PPContext.GNs
                .Where(a => a.Id == id)
                .FirstOrDefault();
        }

        public int GetCount()
        {
            var obj = PPContext.GNs
                .Where(i => i.IsApproved)
                .AsQueryable();
            return obj.Count();
        }

        public List<GameName> GetSearchResult(string q, int page, int pageSize)
        {
            var gps = PPContext.GNs
                .Where(i => i.GameOfName.Contains(q))
                .AsQueryable();

            return gps.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }
    }
}
