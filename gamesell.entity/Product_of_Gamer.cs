using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.entity
{
    public class Product_of_Gamer
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public int GameNameID { get; set; }
        public int DiviceID { get; set; }
        public int GameItemID { get; set; }
        public string Title { get; set; }
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
        public bool IsApproved { get; set; } = true;
        public bool IsPOG { get; set; } = true;
    }
}