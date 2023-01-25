using System;
using System.Collections.Generic;
using System.Linq;
using gamesell.entity;
using System.Threading.Tasks;

namespace gamesellMVC.Models
{
    public class PublisherModel
    {
        public int Id { get; set; }
        public string PublisherName { get; set; }
        public string Img { get; set; }
        public string Back_img { get; set; }
        public bool IsApproved { get; set; }
    }
    public class PublisherListViewModel
    {
        public PageInfo PageInfo { get; set; }
        public List<Publisher> Pubs { get; set; }
    }
}
