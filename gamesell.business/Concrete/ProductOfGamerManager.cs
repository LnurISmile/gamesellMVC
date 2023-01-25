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
    public class ProductOfGamerManager : IProductOfGamerService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductOfGamerManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void Create(Product_of_Gamer entity)
        {
            _unitOfWork.POGs.Create(entity);
            _unitOfWork.Save();
        }

        public void Delete(Product_of_Gamer entity)
        {
            _unitOfWork.POGs.Delete(entity);
            _unitOfWork.Save();
        }

        public Product_of_Gamer GetAdDetails(int id)
        {
            return _unitOfWork.POGs.GetAdDetails(id);
        }

        public List<Product_of_Gamer> GetAll(int page, int pageSize)
        {
            return _unitOfWork.POGs.GetAll(page, pageSize);
        }

        public List<Product_of_Gamer> GetAll()
        {
            return _unitOfWork.POGs.GetAll();
        }

        public Product_of_Gamer GetById(int id)
        {
            return _unitOfWork.POGs.GetById(id);
        }

        public int GetCount()
        {
            return _unitOfWork.POGs.GetCount();
        }

        public List<Product_of_Gamer> GetSearchResult(string q, int page, int pageSize)
        {
            return _unitOfWork.POGs.GetSearchResult(q, page, pageSize);
        }

        public void Update(Product_of_Gamer entity)
        {
            _unitOfWork.POGs.Update(entity);
            _unitOfWork.Save();
        }
    }
}
