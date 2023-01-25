using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.data.Abstract
{
    public interface IActivationCountryRepository : IRepository<ActivationCountry>
    {
        int GetCount();
        List<ActivationCountry> GetSearchResult(string q, int page, int pageSize);
        ActivationCountry GetAdDetails(int id);
    }
}
