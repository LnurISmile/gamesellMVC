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
    public class CartPManager : ICartPService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CartPManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddToCartP(string UserId, int productId)
        {
            var cart = GetCartByUserIdP(UserId);

            if (cart != null)
            {
                var index = cart.CartItemps.FindIndex(i => i.ProductId == productId);
                if (index < 0)
                {
                    cart.CartItemps.Add(new CartItemP() {
                        ProductId = productId,
                        CartId = cart.Id
                    });
                }
                else
                {
                }
                _unitOfWork.Carts.Update(cart);
                _unitOfWork.Save();
            }
        }

        public void DeleteFromCartP(string userId, int productId)
        {
            var cart = GetCartByUserIdP(userId);
            if (cart != null)
            {
                _unitOfWork.Carts.DeleteFromCartP(cart.Id, productId);
            }
        }

        public List<CartP> GetAll()
        {
            return _unitOfWork.Carts.GetAll();
        }

        public CartP GetCartByUserIdP(string userId)
        {
            return _unitOfWork.Carts.GetByUserIdP(userId);
        }

        public void InitializeCart(string userId)
        {
            _unitOfWork.Carts.Create(new CartP() {UserId = userId});
            _unitOfWork.Save();
        }
    }
}