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
    public class CIPManager : ICIPService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CIPManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<CartItemP> GetAll()
        {
            return _unitOfWork.CIs.GetAll();
        }
    }
}
