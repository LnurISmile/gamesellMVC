using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.entity
{
    public class GameItem
    {
        [Key]
        public int Id { get; set; }
        public string GameItemName { get; set; }
        public int GNId { get; set; }
        public bool IsApproved { get; set; }
    }
}
