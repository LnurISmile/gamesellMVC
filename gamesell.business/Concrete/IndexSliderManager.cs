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
    public class IndexSliderManager : IIndexSliderService
    {
        private readonly IUnitOfWork _unitOfWork;
        public IndexSliderManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Create(IndexSlider entity)
        {
            _unitOfWork.ISs.Create(entity);
            _unitOfWork.Save();
        }

        public void Delete(IndexSlider entity)
        {
            _unitOfWork.ISs.Delete(entity);
            _unitOfWork.Save();
        }

        public List<IndexSlider> GetAll(int page, int pageSize)
        {
            return _unitOfWork.ISs.GetAll(page, pageSize);
        }

        public List<IndexSlider> GetAll()
        {
            return _unitOfWork.ISs.GetAll();
        }

        public IndexSlider GetById(int id)
        {
            return _unitOfWork.ISs.GetById(id);
        }

        public void Update(IndexSlider entity)
        {
            _unitOfWork.ISs.Update(entity);
            _unitOfWork.Save();
        }
    }
}
