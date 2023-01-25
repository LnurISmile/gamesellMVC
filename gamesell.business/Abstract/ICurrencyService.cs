using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.business.Abstract
{
    public interface ICurrencyService
    {
        Currency GetById(int id);
        List<Currency> GetAll(int page, int pageSize);
        List<Currency> GetAll();
        Currency GetAdDetails(int id);
        List<Currency> GetSearchResult(string q, int page, int pageSize);
        int GetCount();

        void Create(Currency entity);
        void Update(Currency entity);
        void Delete(Currency entity);
    }
}
