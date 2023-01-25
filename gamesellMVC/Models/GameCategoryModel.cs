using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gamesellMVC.Models
{
    public class GameCategoryModel
    {
        public int Id { get; set; }
        public string GameCategoryName { get; set; }
        public bool IsApproved { get; set; }
    }
    public class GameCategoryListViewModel
    {
        public PageInfo PageInfo { get; set; }
        public List<GameCategory> GCs { get; set; }
    }
}
