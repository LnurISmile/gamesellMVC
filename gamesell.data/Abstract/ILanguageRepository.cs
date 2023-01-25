using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.data.Abstract
{
    public interface ILanguageRepository : IRepository<Language>
    {
        int GetCount();
        List<Language> GetSearchResult(string q, int page, int pageSize);
        Language GetAdDetails(int id);
    }
}
