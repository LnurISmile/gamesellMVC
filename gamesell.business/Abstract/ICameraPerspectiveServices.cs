using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.business.Abstract
{
    public interface ICameraPerspectiveServices
    {
        CameraPerspective GetById(int id);
        List<CameraPerspective> GetAll(int page, int pageSize);
        List<CameraPerspective> GetAll();
        CameraPerspective GetAdDetails(int id);
        List<CameraPerspective> GetSearchResult(string q, int page, int pageSize);
        int GetCount();

        void Create(CameraPerspective entity);
        void Update(CameraPerspective entity);
        void Delete(CameraPerspective entity);
    }
}
