using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gamesellMVC.Models
{
    public class JanraModel
    {
        public int Id { get; set; }
        public string JanraName { get; set; }
        public bool IsApproved { get; set; }
    }
    public class JanraListViewModel
    {
        public PageInfo PageInfo { get; set; }
        public List<Janra> Jans { get; set; }
    }
}
