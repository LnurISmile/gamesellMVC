using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.data.Abstract
{
    public interface IRepository<T>
    {
        T GetById(int id);
        Task<T> GetByIdAsync(int id);
        List<T> GetAll(int page, int pageSize);
        Task<List<T>> GetAllAsync(int page, int pageSize);
        List<T> GetAll();
        Task<List<T>> GetAllAsync();

        void Create(T entity);
        Task CreateAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
