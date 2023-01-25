using gamesell.data.Abstract;
using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.data.Concrete.EfCore
{
    public class EfCoreGameItemRepository :
        EfCoreGenericRepository<GameItem>, IGameItemRepository
    {
        public EfCoreGameItemRepository(PlayPointContext context) : base(context)
        {

        }
        private PlayPointContext PPContext
        {
            get { return context as PlayPointContext; }
        }

        public List<GameItem> GetByIdWithGN(int id)
        {
            return PPContext.GIs
                .Where(i => i.GNId == id)
                .ToList();
        }
    }
}
