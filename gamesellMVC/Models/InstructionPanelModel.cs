using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gamesellMVC.Models
{
    public class InstructionPanelModel
    {
        public int Id { get; set; }
        public string ImgTitle { get; set; }
        public string Img { get; set; }
        public string Text { get; set; }
        public bool Product { get; set; }
        public bool POG { get; set; }
        public bool IsApproved { get; set; }
    }
    public class InstructionPanelListViewModel
    {
        public PageInfo PageInfo { get; set; }
        public List<InstructionPanel> IPs { get; set; }
    }
}
