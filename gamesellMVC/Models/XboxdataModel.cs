using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gamesellMVC.Models
{
    public class XboxdataModel
    {
        public int Id { get; set; }
        public string Img1 { get; set; }
        public string Img2 { get; set; }
        public string Img3 { get; set; }
        public string Img4 { get; set; }
        public string Img5 { get; set; }
        public string Title { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public double Price { get; set; }
        public string SPrice { get; set; }
    }
    public class XboxdataListViewModel
    {
        public DateTime start { get; set; }
        public DateTime expire { get; set; }
        public string day{ get; set; }
        public string month{ get; set; }
        public string year{ get; set; }
        public int Cur { get; set; }
        public double Price { get; set; }
        public List<Currency> Curs { get; set; }
        public PageInfo PageInfo { get; set; }
        public Xboxdata Xboxdata { get; set; }
        public List<Xboxdata> XDs { get; set; }
        public List<Xboxgame> XGs { get; set; }
    }
    public class XboxdataDetailModel
    {
        public Xboxdata Xboxdata { get; set; }
        public List<Xboxgame> Xboxgame { get; set; }
    }
}
