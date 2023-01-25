using gamesell.data.Abstract;
using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.data.Concrete.EfCore
{
    public class EfCoreLanguageRepository :
        EfCoreGenericRepository<Language>, ILanguageRepository
    {
        public EfCoreLanguageRepository(PlayPointContext context) : base(context)
        {

        }
        private PlayPointContext PPContext
        {
            get { return context as PlayPointContext; }
        }

        public Language GetAdDetails(int id)
        {
            return PPContext.Lans
                .Where(a => a.Id == id)
                .FirstOrDefault();
        }

        public int GetCount()
        {
            var obj = PPContext.Lans
                .Where(i => i.IsApproved)
                .AsQueryable();
            return obj.Count();
        }

        public List<Language> GetSearchResult(string q, int page, int pageSize)
        {
            var gps = PPContext.Lans
                .Where(i => i.LanguageName.Contains(q))
                .AsQueryable();

            return gps.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }
    }
}
