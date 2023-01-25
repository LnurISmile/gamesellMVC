using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gamesellMVC.Models
{
    public class CameraPerspectiveModel
    {
        public int Id { get; set; }
        public string CameraPerspevtiveName { get; set; }
        public bool IsApproved { get; set; }
    }
    public class CameraPerspectiveListViewModel
    {
        public PageInfo PageInfo { get; set; }
        public List<CameraPerspective> CPs { get; set; }
    }
}
