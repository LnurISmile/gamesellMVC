using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.entity
{
    public class PurchasedProduct
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public int ProductId { get; set; }
        public List<Product> Products { get; set; }
        public bool IsApproved { get; set; }
        public bool IsXbox { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
    }
}
