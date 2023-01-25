using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.entity
{
    public class Xboxgame
    {
        [Key]
        public int Id { get; set; }
        public string GameName { get; set; }
        public string Img { get; set; }
        public int Priority { get; set; }
    }
}
