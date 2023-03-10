using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.data.Abstract
{
    public interface IGameItemRepository : IRepository<GameItem>
    {
        List<GameItem> GetByIdWithGN(int id);
    }
}
