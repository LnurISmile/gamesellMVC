using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gamesellMVC.Models
{
    public class GameNameModel
    {
        public int Id { get; set; }
        public string GameOfName { get; set; }
        public bool IsApproved { get; set; }
    }
    public class GameNameListViewModel
    {
        public PageInfo PageInfo { get; set; }
        public List<GameName> GNs { get; set; }
    }
}
