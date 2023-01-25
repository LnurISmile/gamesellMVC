using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gamesellMVC.Models
{
    public class ActivationCountryModel
    {
        public int Id { get; set; }
        public string ActivationCountryName { get; set; }
        public bool IsApproved { get; set; }
    }
    public class ActivationCountryListViewModel
    {
        public PageInfo PageInfo { get; set; }
        public List<ActivationCountry> ACs { get; set; }
    }
}
