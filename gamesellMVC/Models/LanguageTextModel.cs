using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gamesellMVC.Models
{
    public class LanguageTextModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int LaguageId { get; set; }
        public string Text { get; set; }
        public bool IsApproved { get; set; }

        public List<Product> Pros { get; set; }
        public List<Language> Lans { get; set; }
    }
    public class LanguageTextListViewModel
    {
        public PageInfo PageInfo { get; set; }
        public List<LanguageText> LTs { get; set; }
        public List<Product> Pros { get; set; }
        public List<Language> Lans { get; set; }
    }
}
