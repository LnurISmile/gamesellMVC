using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.business.Abstract
{
    public interface IDiviceService
    {
        Divice GetById(int id);
        List<Divice> GetAll(int page, int pageSize);
        List<Divice> GetAll();
        Divice GetAdDetails(int id);
        List<Divice> GetSearchResult(string q, int page, int pageSize);
        int GetCount();

        void Create(Divice entity);
        void Update(Divice entity);
        void Delete(Divice entity);
    }
}
