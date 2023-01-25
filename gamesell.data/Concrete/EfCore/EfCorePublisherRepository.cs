using gamesell.data.Abstract;
using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.data.Concrete.EfCore
{
    public class EfCorePublisherRepository :
        EfCoreGenericRepository<Publisher>, IPublisherRepository
    {
        public EfCorePublisherRepository(PlayPointContext context) : base(context)
        {

        }
        private PlayPointContext PPContext
        {
            get { return context as PlayPointContext; }
        }

        public Publisher GetAdDetails(int id)
        {
            return PPContext.Pubs
                .Where(a => a.Id == id)
                .FirstOrDefault();
        }

        public int GetCount()
        {
            var obj = PPContext.Pubs
                .Where(i => i.IsApproved)
                .AsQueryable();
            return obj.Count();
        }

        public List<Publisher> GetSearchResult(string q, int page, int pageSize)
        {
            var gps = PPContext.Pubs
                .Where(i => i.PublisherName.Contains(q))
                .AsQueryable();

            return gps.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }
    }
}
