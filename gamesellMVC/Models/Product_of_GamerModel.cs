using gamesell.entity;
using gamesellMVC.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gamesellMVC.Models
{
    public class Product_of_GamerModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int GameNameID { get; set; }
        public int GameItemID { get; set; }
        public int DiviceID { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public double Price { get; set; }
        public string Slider_videolink { get; set; }
        public string Slider_img1 { get; set; }
        public string Slider_img2 { get; set; }
        public string Slider_img3 { get; set; }
        public string Text { get; set; }
        public int Views { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
        public bool IsApproved { get; set; }

        public List<User> Users { get; set; }
        public List<GameName> Gns { get; set; }
        public List<Divice> Divs { get; set; }
    }
    public class Product_of_GamerListViewModel
    {
        public int Cur { get; set; }
        public List<Currency> Curs { get; set; }
        public PageInfo PageInfo { get; set; }
        public List<Product_of_Gamer> Pogs { get; set; }
        public List<GameName> Gns { get; set; }
        public Product_of_GamerModel POGModel { get; set; }
        public List<Divice> Divs { get; set; }
        public List<User> Users { get; set; }
        public List<InstructionPanel> IPs { get; set; }

        public string q { get; set; }
        public int minValue { get; set; }
        public int maxValue { get; set; }
        public int gnId { get; set; }
        public int giId { get; set; }
        public int order { get; set; }
    }
    public class Product_of_GamerDetailModel
    {
        public int Cur { get; set; }
        public Product_of_Gamer ProductofGamers { get; set; }
        public List<Currency> Curs { get; set; }
        public List<User> Users { get; set; }
        public List<GameName> Gns { get; set; }
        public List<Divice> Divs { get; set; }
    }
}
