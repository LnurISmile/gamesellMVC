using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.entity
{
    public class Developer
    {
        [Key]
        public int Id { get; set; }
        public string DeveloperName { get; set; }
        public string Img { get; set; }
        public string Back_img { get; set; }
        public bool IsApproved { get; set; }
    }
}
