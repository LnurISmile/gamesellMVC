using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gamesellMVC.Models
{
    public class ProductModel
    {
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
        public bool UpComing { get; set; }
        public DateTime ReleaseDate { get; set; } = DateTime.Now;

        public int PlatformID { get; set; }
        public int CategoryID { get; set; }
        public int JanraID { get; set; }
        public int CameraperspectiveID { get; set; }
        public int PublisherID { get; set; }
        public int DeveloperID { get; set; }
        public int Activation_countryID { get; set; }

        public List<Platform> Plats { get; set; }
        public List<GameCategory> GCs { get; set; }
        public List<Janra> Jans { get; set; }
        public List<CameraPerspective> CPs { get; set; }
        public List<Publisher> Pubs { get; set; }
        public List<Developer> Devs { get; set; }
        public List<ActivationCountry> ACs { get; set; }
    }
    public class ProductListViewModel
    {
        public int lastpage { get; set; }
        public int Cur { get; set; }
        public PageInfo PageInfo { get; set; }
        public Xboxdata Xboxdata { get; set; }
        public List<Product> Pros { get; set; }
        public List<Platform> Plats { get; set; }
        public List<GameCategory> GCs { get; set; }
        public List<Janra> Jans { get; set; }
        public List<CameraPerspective> CPs { get; set; }
        public List<Publisher> Pubs { get; set; }
        public List<Developer> Devs { get; set; }
        public List<ActivationCountry> ACs { get; set; }
        public List<InstructionPanel> IPs { get; set; }
        public List<IndexSlider> ISs { get; set; }
        public List<Currency> Curs { get; set; }

        // Home Index
        public List<Product> salegames { get; set; } // 20 or all
        public List<Product> upcominggames { get; set; } // 3
        public List<Product> upcoming5 { get; set; } // 5
        public List<Product> newreleases { get; set; } // 5
        public List<Product> topsellers { get; set; } // 5
        public List<Product> gamesforyou { get; set; } // 21
        public List<Product> indexslider { get; set; } // all

        // search
        public string q { get; set; }
        public int gcId { get; set; }
        public int janId { get; set; }
        public int cpId { get; set; }

    }
    public class ProductDetailModel
    {
        public Product Products { get; set; }
        public int Cur { get; set; }
        public bool have { get; set; }

        public List<Currency> Curs { get; set; }
        public List<Platform> Plats { get; set; }
        public List<GameCategory> GCs { get; set; }
        public List<Janra> Jans { get; set; }
        public List<CameraPerspective> CPs { get; set; }
        public List<Publisher> Pubs { get; set; }
        public List<Developer> Devs { get; set; }
        public List<ActivationCountry> ACs { get; set; }
        public List<Language> Lans { get; set; }
        public List<LanguageText> LTs { get; set; }
    }
}
