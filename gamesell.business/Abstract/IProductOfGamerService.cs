using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.business.Abstract
{
    public interface IProductOfGamerService
    {
        Product_of_Gamer GetById(int id);
        List<Product_of_Gamer> GetAll(int page, int pageSize);
        List<Product_of_Gamer> GetAll();
        Product_of_Gamer GetAdDetails(int id);
        List<Product_of_Gamer> GetSearchResult(string q, int page, int pageSize);
        int GetCount();

        void Create(Product_of_Gamer entity);
        void Update(Product_of_Gamer entity);
        void Delete(Product_of_Gamer entity);
    }
}
