using gamesell.data.Abstract;
using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.data.Concrete.EfCore
{
    public class EfCoreXboxdataRepository :
        EfCoreGenericRepository<Xboxdata>, IXboxdataRepository
    {
        public EfCoreXboxdataRepository(PlayPointContext context) : base(context)
        {

        }
        private PlayPointContext PPContext
        {
            get { return context as PlayPointContext; }
        }

        public Xboxdata GetAdDetails(int id)
        {
            return PPContext.XDs
                .Where(a => a.Id == id)
                .FirstOrDefault();
        }

        public int GetCount()
        {
            var obj = PPContext.XDs
                .AsQueryable();

            return obj.Count();
        }
    }
}
