using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gamesellMVC.Models
{
    public class IndexSliderModel
    {
        public int Id { get; set; }
        public string Img { get; set; }
        public string IndexSliderName { get; set; }
        public string Text { get; set; }
        public string UrlTitle { get; set; }
        public string Url { get; set; }
        public bool IsApproved { get; set; }
    }
    public class IndexSliderListViewModel
    {
        public PageInfo PageInfo { get; set; }
        public List<IndexSlider> ISs { get; set; }
    }
}
