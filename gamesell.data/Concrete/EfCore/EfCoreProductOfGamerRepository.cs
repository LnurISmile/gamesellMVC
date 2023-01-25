using gamesell.data.Abstract;
using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.data.Concrete.EfCore
{
    public class EfCoreProductOfGamerRepository :
        EfCoreGenericRepository<Product_of_Gamer>, IProductOfGamerRepository
    {
        public EfCoreProductOfGamerRepository(PlayPointContext context) : base(context)
        {

        }
        private PlayPointContext PPContext
        {
            get { return context as PlayPointContext; }
        }

        public Product_of_Gamer GetAdDetails(int id)
        {
            return PPContext.POGs
                .Where(a => a.Id == id)
                .FirstOrDefault();
        }

        public int GetCount()
        {
            var obj = PPContext.POGs
                .Where(i => i.IsApproved)
                .AsQueryable();
            return obj.Count();
        }

        public List<Product_of_Gamer> GetSearchResult(string q, int page, int pageSize)
        {
            var gps = PPContext.POGs
                .Where(i => i.Title == q)
                .AsQueryable();

            return gps.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }
    }
}
