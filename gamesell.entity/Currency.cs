using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.entity
{
    public class Currency
    {
        [Key]
        public int Id { get; set; }
        public string CurrencyName { get; set; }
        public string LanguageTag { get; set; }
        public string CurrencyIcon { get; set; }
        public double CurrencyConst { get; set; }
        public string CurrencyStringConst { get; set; }
        public bool IsApproved { get; set; }
    }
}
