using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.business.Abstract
{
    public interface IProductService
    {
        Product GetById(int id);
        List<Product> GetAll(int page, int pageSize);
        List<Product> GetAll();
        List<Product> GetAllNU(int page, int pageSize);
        List<Product> GetAllNU();
        List<Product> GetAllUP(int page, int pageSize);
        List<Product> GetAllNR(int page, int pageSize);
        List<Product> GetAllTS(int page, int pageSize);
        Product GetAdDetails(int id);
        List<Product> GetSearchResult(string q, int page, int pageSize);
        List<Product> GetMultiSearch(int gcId, int janId, int cpId, int page, int pageSize);
        List<Product> GetMultiSearch(string q, int gcId, int janId, int cpId, int page, int pageSize);
        List<Product> GetSearchResult(string q);
        int GetCount();
        int GetCountNU();

        void Create(Product entity);
        void Update(Product entity);
        void Delete(Product entity);

        Task CreateAsync(Product entity);
        Task UpdateAsync(Product entityToUpdate, Product entity);
        Task DeleteAsync(Product entity);

        Task<Product> GetByIdAsync(int id);
        Task<Product> GetAdDetailsAsync(int id);
        Task<List<Product>> GetAllAsync(int page, int pageSize);
        Task<List<Product>> GetAllAsync();
        Task<List<Product>> GetAllNUAsync(int page, int pageSize);
        Task<List<Product>> GetAllNUAsync();
        Task<List<Product>> GetAllUPAsync(int page, int pageSize);
        Task<List<Product>> GetAllNRAsync(int page, int pageSize);
        Task<List<Product>> GetAllTSAsync(int page, int pageSize);
        Task<List<Product>> GetSearchResultAsync(string q, int page, int pageSize);
        Task<List<Product>> GetMultiSearchAsync(int gcId, int janId, int cpId, int page, int pageSize);
        Task<List<Product>> GetMultiSearchAsync(string q, int gcId, int janId, int cpId, int page, int pageSize);
        Task<List<Product>> GetSearchResultAsync(string q);
    }
}
