using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.business.Abstract
{
    public interface IPublisherService
    {
        Publisher GetById(int id);
        List<Publisher> GetAll(int page, int pageSize);
        List<Publisher> GetAll();
        Publisher GetAdDetails(int id);
        List<Publisher> GetSearchResult(string q, int page, int pageSize);
        int GetCount();

        void Create(Publisher entity);
        void Update(Publisher entity);
        void Delete(Publisher entity);
    }
}
