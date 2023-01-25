using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.business.Abstract
{
    public interface ICIPOGService
    {
        List<CartItemPOG> GetAll();
    }
}
