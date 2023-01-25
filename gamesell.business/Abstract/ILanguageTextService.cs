using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.business.Abstract
{
    public interface ILanguageTextService
    {
        LanguageText GetById(int id);
        List<LanguageText> GetAll(int page, int pageSize);
        List<LanguageText> GetAll();

        void Create(LanguageText entity);
        void Update(LanguageText entity);
        void Delete(LanguageText entity);
    }
}
