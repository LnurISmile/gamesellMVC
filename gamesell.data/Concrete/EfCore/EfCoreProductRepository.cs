using gamesell.data.Abstract;
using gamesell.entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.data.Concrete.EfCore
{
    public class EfCoreProductRepository :
        EfCoreGenericRepository<Product>, IProductRepository
    {
        public EfCoreProductRepository(PlayPointContext context) : base(context)
        {

        }
        private PlayPointContext PPContext
        {
            get { return context as PlayPointContext; }
        }

        public Product GetAdDetails(int id)
        {
            return PPContext.Pros
                .Where(a => a.Id == id)
                .FirstOrDefault();
        }

        public List<Product> GetAllNU(int page, int pageSize)
        {
            return PPContext.Set<Product>().Where(i => i.IsApproved && i.UpComing == false).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }
        public List<Product> GetAllNU()
        {
            return PPContext.Set<Product>().Where(i => i.IsApproved && i.UpComing == false).ToList();
        }

        public List<Product> GetAllNR(int page, int pageSize)
        {
            return PPContext.Set<Product>().Where(i => i.IsApproved && i.UpComing == false).OrderByDescending(x => x.ReleaseDate).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public List<Product> GetAllTS(int page, int pageSize)
        {
            return PPContext.Set<Product>().Where(i => i.IsApproved && i.UpComing == false).OrderByDescending(x => x.Number_of_sale).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public List<Product> GetAllUP(int page, int pageSize)
        {
            return PPContext.Set<Product>().Where(i => i.UpComing && i.IsApproved).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public int GetCount()
        {
            var obj = PPContext.Pros
                .Where(i => i.IsApproved)
                .AsQueryable();

            return obj.Count();
        }

        public int GetCountNU()
        {
            var obj = PPContext.Pros
                .Where(i => i.IsApproved && i.UpComing == false)
                .AsQueryable();

            return obj.Count();
        }

        public List<Product> GetMultiSearch(int gcId, int janId, int cpId, int page, int pageSize)
        {
            return PPContext.Set<Product>().Where(i => i.IsApproved && i.UpComing == false && gcId == i.CategoryID && janId == i.JanraID && cpId == i.CameraperspectiveID).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public List<Product> GetMultiSearch(string q, int gcId, int janId, int cpId, int page, int pageSize)
        {
            if ((q != null || q != "") && gcId != 0 && janId == 0 && cpId == 0)
            {
                var gps = PPContext.Pros
                .Where(i => i.IsApproved && i.UpComing == false && (i.Name.Contains(q) || i.Text.Contains(q)) && i.CategoryID == gcId)
                .AsQueryable();

                return gps.Where(i => i.IsApproved).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
            else if ((q != null || q != "") && janId == 0 && janId != 0 && cpId == 0)
            {
                var gps = PPContext.Pros
                .Where(i => i.IsApproved && i.UpComing == false && (i.Name.Contains(q) || i.Text.Contains(q)) && i.JanraID == janId)
                .AsQueryable();

                return gps.Where(i => i.IsApproved).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
            else if ((q != null || q != "") && cpId == 0 && janId == 0 && cpId != 0)
            {
                var gps = PPContext.Pros
                .Where(i => i.IsApproved && i.UpComing == false && (i.Name.Contains(q) || i.Text.Contains(q)) && i.CameraperspectiveID == cpId)
                .AsQueryable();

                return gps.Where(i => i.IsApproved).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
            else if ((q != null || q != "") && gcId != 0 && janId != 0 && cpId == 0)
            {
                var gps = PPContext.Pros
                .Where(i => i.IsApproved && i.UpComing == false && (i.Name.Contains(q) || i.Text.Contains(q)) && i.CategoryID == gcId && i.JanraID == janId)
                .AsQueryable();

                return gps.Where(i => i.IsApproved).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
            else if ((q != null || q != "") && gcId != 0 && janId == 0 && cpId != 0)
            {
                var gps = PPContext.Pros
                .Where(i => i.IsApproved && i.UpComing == false && (i.Name.Contains(q) || i.Text.Contains(q)) && i.CategoryID == gcId && i.CameraperspectiveID == cpId)
                .AsQueryable();

                return gps.Where(i => i.IsApproved).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
            else if((q != null || q != "") && gcId == 0 && janId != 0 && cpId != 0)
            {
                var gps = PPContext.Pros
                .Where(i => i.IsApproved && i.UpComing == false && (i.Name.Contains(q) || i.Text.Contains(q)) && i.JanraID == janId && i.CameraperspectiveID == cpId)
                .AsQueryable();

                return gps.Where(i => i.IsApproved).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
            else
            {
                var gps = PPContext.Pros
                .Where(i => i.IsApproved && i.UpComing == false && (i.Name.Contains(q) || i.Text.Contains(q)))
                .AsQueryable();

                return gps.Where(i => i.IsApproved).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        public List<Product> GetSearchResult(string q, int page, int pageSize)
        {
            var gps = PPContext.Pros
                .Where(i => i.IsApproved && i.UpComing == false && (i.Name.Contains(q) || i.Text.Contains(q)) && i.UpComing == false)
                .AsQueryable();

            return gps.Where(i => i.IsApproved).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public List<Product> GetSearchResult(string q)
        {
            var gps = PPContext.Pros
                .Where(i => i.IsApproved && i.UpComing == false && (i.Name.Contains(q) || i.Text.Contains(q)) && i.UpComing == false)
                .AsQueryable();

            return gps.Where(i => i.IsApproved).ToList();
        }

        public async Task<Product> GetAdDetailsAsync(int id)
        {
            var product = await PPContext.Pros.FindAsync(id);
            if (product != null)
            {
                return product;
            }
            return null;
        }

        public async Task<List<Product>> GetSearchResultAsync(string q, int page, int pageSize)
        {
            var gps = PPContext.Pros
                .Where(i => i.IsApproved && i.UpComing == false && (i.Name.Contains(q) || i.Text.Contains(q)) && i.UpComing == false)
                .AsQueryable();

            return await gps.Where(i => i.IsApproved).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<List<Product>> GetMultiSearchAsync(int gcId, int janId, int cpId, int page, int pageSize)
        {
            return await PPContext.Set<Product>().Where(i => i.IsApproved && i.UpComing == false && gcId == i.CategoryID && janId == i.JanraID && cpId == i.CameraperspectiveID).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<List<Product>> GetMultiSearchAsync(string q, int gcId, int janId, int cpId, int page, int pageSize)
        {
            if ((q != null || q != "") && gcId != 0 && janId == 0 && cpId == 0)
            {
                var gps = PPContext.Pros
                .Where(i => i.IsApproved && i.UpComing == false && (i.Name.Contains(q) || i.Text.Contains(q)) && i.CategoryID == gcId)
                .AsQueryable();

                return await gps.Where(i => i.IsApproved).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            }
            else if ((q != null || q != "") && janId == 0 && janId != 0 && cpId == 0)
            {
                var gps = PPContext.Pros
                .Where(i => i.IsApproved && i.UpComing == false && (i.Name.Contains(q) || i.Text.Contains(q)) && i.JanraID == janId)
                .AsQueryable();

                return await gps.Where(i => i.IsApproved).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            }
            else if ((q != null || q != "") && cpId == 0 && janId == 0 && cpId != 0)
            {
                var gps = PPContext.Pros
                .Where(i => i.IsApproved && i.UpComing == false && (i.Name.Contains(q) || i.Text.Contains(q)) && i.CameraperspectiveID == cpId)
                .AsQueryable();

                return await gps.Where(i => i.IsApproved).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            }
            else if ((q != null || q != "") && gcId != 0 && janId != 0 && cpId == 0)
            {
                var gps = PPContext.Pros
                .Where(i => i.IsApproved && i.UpComing == false && (i.Name.Contains(q) || i.Text.Contains(q)) && i.CategoryID == gcId && i.JanraID == janId)
                .AsQueryable();

                return await gps.Where(i => i.IsApproved).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            }
            else if ((q != null || q != "") && gcId != 0 && janId == 0 && cpId != 0)
            {
                var gps = PPContext.Pros
                .Where(i => i.IsApproved && i.UpComing == false && (i.Name.Contains(q) || i.Text.Contains(q)) && i.CategoryID == gcId && i.CameraperspectiveID == cpId)
                .AsQueryable();

                return await gps.Where(i => i.IsApproved).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            }
            else if ((q != null || q != "") && gcId == 0 && janId != 0 && cpId != 0)
            {
                var gps = PPContext.Pros
                .Where(i => i.IsApproved && i.UpComing == false && (i.Name.Contains(q) || i.Text.Contains(q)) && i.JanraID == janId && i.CameraperspectiveID == cpId)
                .AsQueryable();

                return await gps.Where(i => i.IsApproved).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            }
            else
            {
                var gps = PPContext.Pros
                .Where(i => i.IsApproved && i.UpComing == false && (i.Name.Contains(q) || i.Text.Contains(q)))
                .AsQueryable();

                return await gps.Where(i => i.IsApproved).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            }
        }

        public async Task<List<Product>> GetSearchResultAsync(string q)
        {
            var gps = PPContext.Pros
                .Where(i => i.IsApproved && i.UpComing == false && (i.Name.Contains(q) || i.Text.Contains(q)) && i.UpComing == false)
                .AsQueryable();

            return await gps.Where(i => i.IsApproved).ToListAsync();
        }

        public async Task<List<Product>> GetAllUPAsync(int page, int pageSize)
        {
            var rand = new Random();
            var proup =  await PPContext.Set<Product>().Where(i => i.UpComing && i.IsApproved).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return proup.OrderBy(o => rand.Next()).ToList();
        }

        public async Task<List<Product>> GetAllNRAsync(int page, int pageSize)
        {
            return await PPContext.Set<Product>().Where(i => i.IsApproved && i.UpComing == false).OrderByDescending(x => x.ReleaseDate).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<List<Product>> GetAllTSAsync(int page, int pageSize)
        {
            return await PPContext.Set<Product>().Where(i => i.IsApproved && i.UpComing == false).OrderByDescending(x => x.Number_of_sale).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<List<Product>> GetAllNUAsync(int page, int pageSize)
        {
            var rand = new Random();
            var pronu = await PPContext.Set<Product>().Where(i => i.IsApproved && i.UpComing == false).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return pronu.OrderBy(o => rand.Next()).ToList();
        }

        public async Task<List<Product>> GetAllNUAsync()
        {
            var rand = new Random();
            var pronu = await PPContext.Set<Product>().Where(i => i.IsApproved && i.UpComing == false).ToListAsync();
            return pronu.OrderBy(o => rand.Next()).ToList();
        }
    }
}
