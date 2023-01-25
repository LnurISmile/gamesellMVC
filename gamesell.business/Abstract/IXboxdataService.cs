using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.business.Abstract
{
    public interface IXboxdataService
    {
        Xboxdata GetById(int id);
        List<Xboxdata> GetAll(int page, int pageSize);
        List<Xboxdata> GetAll();
        Xboxdata GetAdDetails(int id);
        int GetCount();

        void Create(Xboxdata entity);
        void Update(Xboxdata entity);
        void Delete(Xboxdata entity);
    }
}
