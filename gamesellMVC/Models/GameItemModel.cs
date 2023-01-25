using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gamesellMVC.Models
{
    public class GameItemModel
    {
        public int Id { get; set; }
        public string GameItemName { get; set; }
        public int GNId { get; set; }
        public bool IsApproved { get; set; }

        public List<GameName> GNs { get; set; }
    }
    public class GameItemListViewModel
    {
        public PageInfo PageInfo { get; set; }
        public List<GameItem> GIs { get; set; }
    }
}
