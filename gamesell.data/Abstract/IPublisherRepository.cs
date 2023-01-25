using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.data.Abstract
{
    public interface IPublisherRepository : IRepository<Publisher>
    {
        int GetCount();
        List<Publisher> GetSearchResult(string q, int page, int pageSize);
        Publisher GetAdDetails(int id);
    }
}
