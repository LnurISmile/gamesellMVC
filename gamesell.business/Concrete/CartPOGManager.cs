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
    public class CartPOGManager : ICartPOGService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CartPOGManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddToCartPOG(string UserId, int pogId)
        {
            var cart = GetCartByUserIdPOG(UserId);

            if (cart != null)
            {
                var index = cart.CartItempogs.FindIndex(i => i.POGId == pogId);
                if (index < 0)
                {
                    cart.CartItempogs.Add(new CartItemPOG()
                    {
                        POGId = pogId,
                        CartId = cart.Id
                    });
                }
                else
                {
                }
                _unitOfWork.Cartpogs.Update(cart);
                _unitOfWork.Save();
            }
        }

        public void DeleteFromCartPOG(string userId, int pogId)
        {
            var cart = GetCartByUserIdPOG(userId);
            if (cart != null)
            {
                _unitOfWork.Cartpogs.DeleteFromCartPOG(cart.Id, pogId);
            }
        }

        public List<CartPOG> GetAll()
        {
            return _unitOfWork.Cartpogs.GetAll();
        }

        public CartPOG GetCartByUserIdPOG(string userId)
        {
            return _unitOfWork.Cartpogs.GetByUserIdPOG(userId);
        }

        public void InitializeCartPOG(string userId)
        {
            _unitOfWork.Cartpogs.Create(new CartPOG() { UserId = userId });
            _unitOfWork.Save();
        }
    }
}