using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.business.Abstract
{
    public interface IPlatformService
    {
        Platform GetById(int id);
        List<Platform> GetAll(int page, int pageSize);
        List<Platform> GetAll();
        Platform GetAdDetails(int id);
        List<Platform> GetSearchResult(string q, int page, int pageSize);
        int GetCount();

        void Create(Platform entity);
        void Update(Platform entity);
        void Delete(Platform entity);
    }
}
