using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.data.Abstract
{
    public interface ICameraPerspectiveRepository : IRepository<CameraPerspective>
    {
        int GetCount();
        List<CameraPerspective> GetSearchResult(string q, int page, int pageSize);
        CameraPerspective GetAdDetails(int id);
    }
}
