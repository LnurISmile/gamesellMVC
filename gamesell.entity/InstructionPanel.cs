using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.entity
{
    public class InstructionPanel
    {
        [Key]
        public int Id { get; set; }
        public string ImgTitle { get; set; }
        public string Img { get; set; }
        public string Text { get; set; }
        public bool Product { get; set; }
        public bool POG { get; set; }
        public bool IsApproved { get; set; }
    }
}
