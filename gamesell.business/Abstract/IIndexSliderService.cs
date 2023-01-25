using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.business.Abstract
{
    public interface IIndexSliderService
    {
        IndexSlider GetById(int id);
        List<IndexSlider> GetAll(int page, int pageSize);
        List<IndexSlider> GetAll();

        void Create(IndexSlider entity);
        void Update(IndexSlider entity);
        void Delete(IndexSlider entity);
    }
}
