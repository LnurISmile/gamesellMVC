using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.business.Abstract
{
    public interface ILanguageService
    {
        Language GetById(int id);
        List<Language> GetAll(int page, int pageSize);
        List<Language> GetAll();
        Language GetAdDetails(int id);
        List<Language> GetSearchResult(string q, int page, int pageSize);
        int GetCount();

        void Create(Language entity);
        void Update(Language entity);
        void Delete(Language entity);
    }
}
