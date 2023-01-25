using gamesell.business.Abstract;
using gamesell.data.Abstract;
using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.business.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void Create(Product entity)
        {
            _unitOfWork.Pros.Create(entity);
            _unitOfWork.Save();
        }

        public async Task CreateAsync(Product entity)
        {
            await _unitOfWork.Pros.CreateAsync(entity);
            await _unitOfWork.SaveAsync();
        }

        public void Delete(Product entity)
        {
            _unitOfWork.Pros.Delete(entity);
            _unitOfWork.Save();
        }

        public async Task DeleteAsync(Product entity)
        {
            _unitOfWork.Pros.Delete(entity);
            await _unitOfWork.SaveAsync();
        }

        public Product GetAdDetails(int id)
        {
            return _unitOfWork.Pros.GetAdDetails(id);
        }

        public async Task<Product> GetAdDetailsAsync(int id)
        {
            return await _unitOfWork.Pros.GetAdDetailsAsync(id);
        }

        public List<Product> GetAll(int page, int pageSize)
        {
            return _unitOfWork.Pros.GetAll(page, pageSize);
        }

        public List<Product> GetAll()
        {
            return _unitOfWork.Pros.GetAll();
        }

        public async Task<List<Product>> GetAllAsync(int page, int pageSize)
        {
            return await _unitOfWork.Pros.GetAllAsync(page, pageSize);
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _unitOfWork.Pros.GetAllAsync();
        }

        public List<Product> GetAllNR(int page, int pageSize)
        {
            return _unitOfWork.Pros.GetAllNR(page, pageSize);
        }

        public async Task<List<Product>> GetAllNRAsync(int page, int pageSize)
        {
            return await _unitOfWork.Pros.GetAllNRAsync(page, pageSize);
        }

        public List<Product> GetAllNU(int page, int pageSize)
        {
            return _unitOfWork.Pros.GetAllNU(page, pageSize);
        }

        public List<Product> GetAllNU()
        {
            return _unitOfWork.Pros.GetAllNU();
        }

        public async Task<List<Product>> GetAllNUAsync(int page, int pageSize)
        {
            return await _unitOfWork.Pros.GetAllNUAsync(page, pageSize);
        }

        public async Task<List<Product>> GetAllNUAsync()
        {
            return await _unitOfWork.Pros.GetAllNUAsync();
        }

        public List<Product> GetAllTS(int page, int pageSize)
        {
            return _unitOfWork.Pros.GetAllTS(page, pageSize);
        }

        public async Task<List<Product>> GetAllTSAsync(int page, int pageSize)
        {
            return await _unitOfWork.Pros.GetAllTSAsync(page, pageSize);
        }

        public List<Product> GetAllUP(int page, int pageSize)
        {
            return _unitOfWork.Pros.GetAllUP(page, pageSize);
        }

        public async Task<List<Product>> GetAllUPAsync(int page, int pageSize)
        {
            return await _unitOfWork.Pros.GetAllUPAsync(page, pageSize);
        }

        public Product GetById(int id)
        {
            return _unitOfWork.Pros.GetById(id);
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _unitOfWork.Pros.GetByIdAsync(id);
        }

        public int GetCount()
        {
            return _unitOfWork.Pros.GetCount();
        }

        public int GetCountNU()
        {
            return _unitOfWork.Pros.GetCountNU();
        }

        public List<Product> GetMultiSearch(int gcId, int janId, int cpId, int page, int pageSize)
        {
            return _unitOfWork.Pros.GetMultiSearch(gcId, janId, cpId, page, pageSize);
        }

        public List<Product> GetMultiSearch(string q, int gcId, int janId, int cpId, int page, int pageSize)
        {
            return _unitOfWork.Pros.GetMultiSearch(q, gcId, janId, cpId, page, pageSize);
        }

        public async Task<List<Product>> GetMultiSearchAsync(int gcId, int janId, int cpId, int page, int pageSize)
        {
            return await _unitOfWork.Pros.GetMultiSearchAsync(gcId, janId, cpId, page, pageSize);
        }

        public async Task<List<Product>> GetMultiSearchAsync(string q, int gcId, int janId, int cpId, int page, int pageSize)
        {
            return await _unitOfWork.Pros.GetMultiSearchAsync(q, gcId, janId, cpId, page, pageSize);
        }

        public List<Product> GetSearchResult(string q, int page, int pageSize)
        {
            return _unitOfWork.Pros.GetSearchResult(q, page, pageSize);
        }

        public List<Product> GetSearchResult(string q)
        {
            return _unitOfWork.Pros.GetSearchResult(q);
        }

        public async Task<List<Product>> GetSearchResultAsync(string q, int page, int pageSize)
        {
            return await _unitOfWork.Pros.GetSearchResultAsync(q, page, pageSize);
        }

        public async Task<List<Product>> GetSearchResultAsync(string q)
        {
            return await _unitOfWork.Pros.GetSearchResultAsync(q);
        }

        public void Update(Product entity)
        {
            _unitOfWork.Pros.Update(entity);
            _unitOfWork.Save();
        }

        public async Task UpdateAsync(Product entityToUpdate, Product entity)
        {
            entityToUpdate.Name = entity.Name;
            entityToUpdate.Login = entity.Login;
            entityToUpdate.Password = entity.Password;
            entityToUpdate.Key = entity.Key;
            entityToUpdate.Contenttype = entity.Contenttype;
            entityToUpdate.Price = entity.Price;
            entityToUpdate.Company_name = entity.Company_name;
            entityToUpdate.Activation_zone = entity.Activation_zone;
            entityToUpdate.Onlineornot = entity.Onlineornot;
            entityToUpdate.Signleplayer = entity.Signleplayer;
            entityToUpdate.Multiplayer = entity.Multiplayer;
            entityToUpdate.Co_op = entity.Co_op;
            entityToUpdate.Type_active = entity.Type_active;
            entityToUpdate.twoD = entity.twoD;
            entityToUpdate.threeD = entity.threeD;
            entityToUpdate.VR = entity.VR;
            entityToUpdate.IndexSlider = entity.IndexSlider;
            entityToUpdate.Main_img = entity.Main_img;
            entityToUpdate.Slider_videolink = entity.Slider_videolink;
            entityToUpdate.Slider_img1 = entity.Slider_img1;
            entityToUpdate.Slider_img2 = entity.Slider_img2;
            entityToUpdate.Slider_img3 = entity.Slider_img3;
            entityToUpdate.Text = entity.Text;
            entityToUpdate.Discount_percent = entity.Discount_percent;
            entityToUpdate.ConstNumber = entity.ConstNumber;
            entityToUpdate.Instock = entity.Instock;
            entityToUpdate.Stocksize = entity.Stocksize;
            entityToUpdate.Number_of_sale = entity.Number_of_sale;
            entityToUpdate.Url = entity.Url;
            entityToUpdate.IsApproved = entity.IsApproved;
            entityToUpdate.IsProduct = entity.IsProduct;
            entityToUpdate.UpComing = entity.UpComing;
            entityToUpdate.ReleaseDate = entity.ReleaseDate;
            entityToUpdate.PlatformID = entity.PlatformID;
            entityToUpdate.CategoryID = entity.CategoryID;
            entityToUpdate.JanraID = entity.JanraID;
            entityToUpdate.CameraperspectiveID = entity.CameraperspectiveID;
            entityToUpdate.PublisherID = entity.PublisherID;
            entityToUpdate.DeveloperID = entity.DeveloperID;
            entityToUpdate.Activation_countryID = entity.Activation_countryID;

            await _unitOfWork.SaveAsync();
        }
    }
}
