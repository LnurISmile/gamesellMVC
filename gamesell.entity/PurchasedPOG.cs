using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.entity
{
    public class PurchasedPOG
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public int POGId { get; set; }
        public List<Product_of_Gamer> POGs { get; set; }
        public bool IsApproved { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
    }
}
