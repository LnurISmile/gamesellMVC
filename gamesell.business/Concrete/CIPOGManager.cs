using gamesell.business.Abstract;
using gamesell.data.Abstract;
using gamesell.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamesell.business.Concrete
{
    public class CIPOGManager : ICIPOGService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CIPOGManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<CartItemPOG> GetAll()
        {
            return _unitOfWork.CIpogs.GetAll();
        }
    }
}
