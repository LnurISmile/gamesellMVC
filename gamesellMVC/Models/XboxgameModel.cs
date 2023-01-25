using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gamesellMVC.Models
{
    public class XboxgameModel
    {
        public int Id { get; set; }
        public string GameName { get; set; }
        public string Img { get; set; }
        public int Priority { get; set; }
    }
    public class XboxgameListViewModel
    {
        public PageInfo PageInfo { get; set; }
        public List<Xboxgame> XGs { get; set; }
    }
}
