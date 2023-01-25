using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.business.Abstract
{
    public interface IDeveloperService
    {
        Developer GetById(int id);
        List<Developer> GetAll(int page, int pageSize);
        List<Developer> GetAll();
        Developer GetAdDetails(int id);
        List<Developer> GetSearchResult(string q, int page, int pageSize);
        int GetCount();

        void Create(Developer entity);
        void Update(Developer entity);
        void Delete(Developer entity);
    }
}
