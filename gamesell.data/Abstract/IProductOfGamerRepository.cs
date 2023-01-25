using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.data.Abstract
{
    public interface IProductOfGamerRepository : IRepository<Product_of_Gamer>
    {
        int GetCount();
        List<Product_of_Gamer> GetSearchResult(string q, int page, int pageSize);
        Product_of_Gamer GetAdDetails(int id);
    }
}
