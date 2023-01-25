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
    public class LanguageManager : ILanguageService
    {
        private readonly IUnitOfWork _unitOfWork;
        public LanguageManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void Create(Language entity)
        {
            _unitOfWork.Lans.Create(entity);
            _unitOfWork.Save();
        }

        public void Delete(Language entity)
        {
            _unitOfWork.Lans.Delete(entity);
            _unitOfWork.Save();
        }

        public Language GetAdDetails(int id)
        {
            return _unitOfWork.Lans.GetAdDetails(id);
        }

        public List<Language> GetAll(int page, int pageSize)
        {
            return _unitOfWork.Lans.GetAll(page, pageSize);
        }

        public List<Language> GetAll()
        {
            return _unitOfWork.Lans.GetAll();
        }

        public Language GetById(int id)
        {
            return _unitOfWork.Lans.GetById(id);
        }

        public int GetCount()
        {
            return _unitOfWork.Lans.GetCount();
        }

        public List<Language> GetSearchResult(string q, int page, int pageSize)
        {
            return _unitOfWork.Lans.GetSearchResult(q, page, pageSize);
        }

        public void Update(Language entity)
        {
            _unitOfWork.Lans.Update(entity);
            _unitOfWork.Save();
        }
    }
}
