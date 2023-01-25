using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.entity
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Key { get; set; }
        public int Contenttype { get; set; }
        public double Price { get; set; }
        public string Company_name { get; set; }
        public string Activation_zone { get; set; }
        public bool Onlineornot { get; set; }
        public bool Signleplayer { get; set; }
        public bool Multiplayer { get; set; }
        public bool Co_op { get; set; }
        public bool Type_active { get; set; }
        public bool twoD { get; set; }
        public bool threeD { get; set; }
        public bool VR { get; set; }
        public bool IndexSlider { get; set; }
        public string Main_img { get; set; }
        public string Slider_videolink { get; set; }
        public string Slider_img1 { get; set; }
        public string Slider_img2 { get; set; }
        public string Slider_img3 { get; set; }
        public string Text { get; set; }
        public int Discount_percent { get; set; }
        public double ConstNumber { get; set; }
        public bool Instock { get; set; }
        public int Stocksize { get; set; }
        public int Number_of_sale { get; set; }
        public string Url { get; set; }
        public bool IsApproved { get; set; }
        public bool IsProduct { get; set; } = true;
        public bool UpComing { get; set; }
        public DateTime ReleaseDate { get; set; } = DateTime.Now;

        public int PlatformID { get; set; }
        public int CategoryID { get; set; }
        public int JanraID { get; set; }
        public int CameraperspectiveID { get; set; }
        public int PublisherID { get; set; }
        public int DeveloperID { get; set; }
        public int Activation_countryID { get; set; }
    }
}
