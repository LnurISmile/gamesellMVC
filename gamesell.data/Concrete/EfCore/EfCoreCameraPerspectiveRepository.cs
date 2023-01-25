using gamesell.data.Abstract;
using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.data.Concrete.EfCore
{
    public class EfCoreCameraPerspectiveRepository :
        EfCoreGenericRepository<CameraPerspective>, ICameraPerspectiveRepository
    {
        public EfCoreCameraPerspectiveRepository(PlayPointContext context) : base(context)
        {

        }
        private PlayPointContext PPContext
        {
            get { return context as PlayPointContext; }
        }

        public CameraPerspective GetAdDetails(int id)
        {
            return PPContext.CPs
                .Where(a => a.Id == id)
                .FirstOrDefault();
        }

        public int GetCount()
        {
            var obj = PPContext.CPs
                .Where(i => i.IsApproved)
                .AsQueryable();
            return obj.Count();
        }

        public List<CameraPerspective> GetSearchResult(string q, int page, int pageSize)
        {
            var gps = PPContext.CPs
                .Where(i => i.CameraPerspevtiveName.Contains(q))
                .AsQueryable();

            return gps.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }
    }
}
