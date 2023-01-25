using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gamesellMVC.Models
{
    public class PlatformModel
    {
        public int Id { get; set; }
        public string PlatformName { get; set; }
        public string Link { get; set; }
        public bool IsApproved { get; set; }
    }
    public class PlatformListViewModel
    {
        public PageInfo PageInfo { get; set; }
        public List<Platform> Plats { get; set; }
    }
}
