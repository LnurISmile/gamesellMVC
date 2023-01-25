using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.data.Abstract
{
    public interface IXboxgameRepository : IRepository<Xboxgame>
    {
        List<Xboxgame> GetAllforP(int page, int pageSize);
        int GetCount();
    }
}
