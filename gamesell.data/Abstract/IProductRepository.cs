using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.data.Abstract
{
    public interface IProductRepository : IRepository<Product>
    {
        int GetCount();
        int GetCountNU();
        List<Product> GetSearchResult(string q, int page , int pageSize);
        List<Product> GetMultiSearch(int gcId, int janId, int cpId, int page , int pageSize);
        List<Product> GetMultiSearch(string q, int gcId, int janId, int cpId, int page, int pageSize);
        List<Product> GetSearchResult(string q);
        Product GetAdDetails(int id);
        List<Product> GetAllUP(int page, int pageSize);
        List<Product> GetAllNR(int page, int pageSize);
        List<Product> GetAllTS(int page, int pageSize);
        List<Product> GetAllNU(int page, int pageSize);
        List<Product> GetAllNU();

        Task<Product> GetAdDetailsAsync(int id);
        Task<List<Product>> GetSearchResultAsync(string q, int page, int pageSize);
        Task<List<Product>> GetMultiSearchAsync(int gcId, int janId, int cpId, int page, int pageSize);
        Task<List<Product>> GetMultiSearchAsync(string q, int gcId, int janId, int cpId, int page, int pageSize);
        Task<List<Product>> GetSearchResultAsync(string q);
        Task<List<Product>> GetAllUPAsync(int page, int pageSize);
        Task<List<Product>> GetAllNRAsync(int page, int pageSize);
        Task<List<Product>> GetAllTSAsync(int page, int pageSize);
        Task<List<Product>> GetAllNUAsync(int page, int pageSize);
        Task<List<Product>> GetAllNUAsync();
    }
}
