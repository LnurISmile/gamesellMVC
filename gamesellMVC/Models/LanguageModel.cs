using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gamesellMVC.Models
{
    public class LanguageModel
    {
        public int Id { get; set; }
        public string LanguageName { get; set; }
        public string LanguageTag { get; set; }
        public string LanguageIcon { get; set; }
        public bool IsApproved { get; set; }
    }
    public class LanguageListViewModel
    {
        public PageInfo PageInfo { get; set; }
        public List<Language> Langs { get; set; }
    }
}
