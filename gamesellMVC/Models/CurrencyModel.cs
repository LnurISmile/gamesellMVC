using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gamesellMVC.Models
{
    public class CurrencyModel
    {
        public int Id { get; set; }
        public string CurrencyName { get; set; }
        public string LanguageTag { get; set; }
        public double CurrencyConst { get; set; }
        public string CurrencyStringConst { get; set; }
        public string CurrencyIcon { get; set; }
        public bool IsApproved { get; set; }
    }

    public class CurrencyListViewModel
    {
        public PageInfo PageInfo { get; set; }
        public List<Currency> Curs { get; set; }
    }
}
