using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.business.Abstract
{
    public interface IJanraService
    {
        Janra GetById(int id);
        List<Janra> GetAll(int page, int pageSize);
        List<Janra> GetAll();
        Janra GetAdDetails(int id);
        List<Janra> GetSearchResult(string q, int page, int pageSize);
        int GetCount();

        void Create(Janra entity);
        void Update(Janra entity);
        void Delete(Janra entity);
    }
}
