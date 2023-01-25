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
    public class LanguageTextManager : ILanguageTextService
    {
        private readonly IUnitOfWork _unitOfWork;
        public LanguageTextManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Create(LanguageText entity)
        {
            _unitOfWork.LTs.Create(entity);
            _unitOfWork.Save();
        }

        public void Delete(LanguageText entity)
        {
            _unitOfWork.LTs.Delete(entity);
            _unitOfWork.Save();
        }

        public List<LanguageText> GetAll(int page, int pageSize)
        {
            return _unitOfWork.LTs.GetAll(page, pageSize);
        }

        public List<LanguageText> GetAll()
        {
            return _unitOfWork.LTs.GetAll();
        }

        public LanguageText GetById(int id)
        {
            return _unitOfWork.LTs.GetById(id);
        }

        public void Update(LanguageText entity)
        {
            _unitOfWork.LTs.Update(entity);
            _unitOfWork.Save();
        }
    }
}
