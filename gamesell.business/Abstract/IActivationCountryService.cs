using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.business.Abstract
{
    public interface IActivationCountryService
    {
        ActivationCountry GetById(int id);
        List<ActivationCountry> GetAll(int page, int pageSize);
        List<ActivationCountry> GetAll();
        ActivationCountry GetAdDetails(int id);
        List<ActivationCountry> GetSearchResult(string q, int page, int pageSize);
        int GetCount();

        void Create(ActivationCountry entity);
        void Update(ActivationCountry entity);
        void Delete(ActivationCountry entity);
    }
}
