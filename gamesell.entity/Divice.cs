using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.entity
{
    public class Divice
    {
        [Key]
        public int Id { get; set; }
        public string DiviceName { get; set; }
        public bool IsApproved { get; set; }
    }
}
