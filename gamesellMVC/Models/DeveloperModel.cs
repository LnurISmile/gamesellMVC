using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gamesellMVC.Models
{
    public class DeveloperModel
    {
        public int Id { get; set; }
        public string DeveloperName { get; set; }
        public string Img { get; set; }
        public string Back_img { get; set; }
        public bool IsApproved { get; set; }
    }
    public class DeveloperListViewModel
    {
        public PageInfo PageInfo { get; set; }
        public List<Developer> Devs { get; set; }
    }
}
