using gamesell.data.Abstract;
using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.data.Concrete.EfCore
{
    public class EfCoreGameCategoryRepository :
        EfCoreGenericRepository<GameCategory>, IGameCategoryRepository
    {
        public EfCoreGameCategoryRepository(PlayPointContext context) : base(context)
        {

        }
        private PlayPointContext PPContext
        {
            get { return context as PlayPointContext; }
        }

        public GameCategory GetAdDetails(int id)
        {
            return PPContext.GCs
                .Where(a => a.Id == id)
                .FirstOrDefault();
        }

        public int GetCount()
        {
            var obj = PPContext.GCs
                .Where(i => i.IsApproved)
                .AsQueryable();
            return obj.Count();
        }

        public List<GameCategory> GetSearchResult(string q, int page, int pageSize)
        {
            var gps = PPContext.GCs
                .Where(i => i.GameCategoryName.Contains(q))
                .AsQueryable();

            return gps.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }
    }
}
