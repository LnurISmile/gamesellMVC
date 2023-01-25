using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gamesellMVC.Models
{
    public class DiviceModel
    {
        public int Id { get; set; }
        public string DiviceName { get; set; }
        public bool IsApproved { get; set; }
    }
    public class DiviceListViewModel
    {
        public PageInfo PageInfo { get; set; }
        public List<Divice> Divs { get; set; }
    }
}
