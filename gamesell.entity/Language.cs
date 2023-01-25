using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.entity
{
    public class Language
    {
        [Key]
        public int Id { get; set; }
        public string LanguageName { get; set; }
        public string LanguageIcon { get; set; }
        public string LanguageTag { get; set; }
        public bool IsApproved { get; set; }
    }
}
