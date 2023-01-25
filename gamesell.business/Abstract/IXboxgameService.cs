using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.business.Abstract
{
    public interface IXboxgameService
    {
        Xboxgame GetById(int id);
        List<Xboxgame> GetAll(int page, int pageSize);
        List<Xboxgame> GetAll();
        List<Xboxgame> GetAllforP(int page, int pageSize);
        int GetCount();

        void Create(Xboxgame entity);
        void Update(Xboxgame entity);
        void Delete(Xboxgame entity);
    }
}
