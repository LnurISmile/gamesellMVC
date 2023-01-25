using gamesell.data.Abstract;
using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.data.Concrete.EfCore
{
    public class EfCoreXboxgameRepository :
        EfCoreGenericRepository<Xboxgame>, IXboxgameRepository
    {
        public EfCoreXboxgameRepository(PlayPointContext context) : base(context)
        {

        }
        private PlayPointContext PPContext
        {
            get { return context as PlayPointContext; }
        }

        public List<Xboxgame> GetAllforP(int page, int pageSize)
        {
            return PPContext.Set<Xboxgame>().OrderByDescending(x => x.Priority).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public int GetCount()
        {
            var obj = PPContext.XGs
                .AsQueryable();
            return obj.Count();
        }
    }
}
