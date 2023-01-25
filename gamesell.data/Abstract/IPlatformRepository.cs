﻿using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.data.Abstract
{
    public interface IPlatformRepository : IRepository<Platform>
    {
        int GetCount();
        List<Platform> GetSearchResult(string q, int page, int pageSize);
        Platform GetAdDetails(int id);
    }
}
