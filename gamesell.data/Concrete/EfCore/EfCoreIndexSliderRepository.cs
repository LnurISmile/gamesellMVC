﻿using gamesell.data.Abstract;
using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.data.Concrete.EfCore
{
    public class EfCoreIndexSliderRepository :
        EfCoreGenericRepository<IndexSlider>, IIndexSliderRepository
    {
        public EfCoreIndexSliderRepository(PlayPointContext context) : base(context)
        {

        }
        private PlayPointContext PPContext
        {
            get { return context as PlayPointContext; }
        }
    }
}
