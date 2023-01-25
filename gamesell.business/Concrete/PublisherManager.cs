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
    public class PublisherManager : IPublisherService
    {
        private readonly IUnitOfWork _unitOfWork;
        public PublisherManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void Create(Publisher entity)
        {
            _unitOfWork.Pubs.Create(entity);
            _unitOfWork.Save();
        }

        public void Delete(Publisher entity)
        {
            _unitOfWork.Pubs.Delete(entity);
            _unitOfWork.Save();
        }

        public Publisher GetAdDetails(int id)
        {
            return _unitOfWork.Pubs.GetAdDetails(id);
        }

        public List<Publisher> GetAll(int page, int pageSize)
        {
            return _unitOfWork.Pubs.GetAll(page, pageSize);
        }

        public List<Publisher> GetAll()
        {
            return _unitOfWork.Pubs.GetAll();
        }

        public Publisher GetById(int id)
        {
            return _unitOfWork.Pubs.GetById(id);
        }

        public int GetCount()
        {
            return _unitOfWork.Pubs.GetCount();
        }

        public List<Publisher> GetSearchResult(string q, int page, int pageSize)
        {
            return _unitOfWork.Pubs.GetSearchResult(q, page, pageSize);
        }

        public void Update(Publisher entity)
        {
            _unitOfWork.Pubs.Update(entity);
            _unitOfWork.Save();
        }
    }
}
