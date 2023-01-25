using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.entity
{
    public class IndexSlider
    {
        [Key]
        public int Id { get; set; }
        public string Img { get; set; }
        public string IndexSliderName { get; set; }
        public string Text { get; set; }
        public string UrlTitle { get; set; }
        public string Url { get; set; }
        public bool IsApproved { get; set; }
    }
}
