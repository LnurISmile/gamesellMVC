using gamesell.business.Abstract;
using gamesellMVC.Identity;
using gamesellMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gamesellMVC.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private ICurrencyService _curService;
        private ICartPService _cartService;
        private ICartPOGService _cartpogService;
        private UserManager<User> _userService;
        private IGameNameService _gnService;
        private IPlatformService _platService;
        public CartController(ICartPService cartService, UserManager<User> userService,
                              IPlatformService platService, IGameNameService gnService,
                              ICartPOGService cartpogService, ICurrencyService curService)
        {
            _cartService = cartService;
            _userService = userService;
            _gnService = gnService;
            _platService = platService;
            _cartpogService = cartpogService;
            _curService = curService;
        }
        
        public async Task<IActionResult> CartProduct()
        {
            var user = await _userService.FindByNameAsync(User.Identity.Name);
            var cart = _cartService.GetCartByUserIdP(_userService.GetUserId(User));
            var entity1 = _platService.GetAll();
            var entity2 = _gnService.GetAll();

            var cartModel = new CartPModel()
            {
                Curs = _curService.GetAll().ToList(),
                Cur = user.currencyID,
                CartPId = cart.Id,
                CartItemsP = cart.CartItemps.Select(i => new CartItemPModel()
                {
                    CartItemId = i.Id,
                    ProductId = i.ProductId,
                    Name = i.Product.Name,
                    Price = (double)i.Product.Price,
                    Img = i.Product.Main_img,
                    Discount = i.Product.Discount_percent,
                    Const = i.Product.Price - (i.Product.Price * i.Product.Discount_percent) / 100,
                    Platform = i.Product.PlatformID,
                    
                }).ToList(),
                Plats = entity1.ToList()
            };
            return View(cartModel);
        }

        [HttpPost]
        public IActionResult AddtoCartP(int productId)
        {
            var userId = _userService.GetUserId(User);
            _cartService.AddToCartP(userId, productId);
            return Redirect($"/product/details/{productId}");
        }
        [HttpPost]
        public void AddtoCartPvoid(int productId)
        {
            var userId = _userService.GetUserId(User);
            _cartService.AddToCartP(userId, productId);
        }

        [HttpPost]
        public IActionResult AddtoCartPOG(int pogId)
        {
            var userId = _userService.GetUserId(User);
            _cartpogService.AddToCartPOG(userId, pogId);
            return Redirect($"/product/pog/details/{pogId}");
        }

        [HttpPost]
        public IActionResult DeleteFromCartP(int productId)
        {
            var userId = _userService.GetUserId(User);
            _cartService.DeleteFromCartP(userId, productId);
            return RedirectToAction("CartProduct");
        }

        [HttpGet]
        public void DeleteFromCP(int productId)
        {
            var userId = _userService.GetUserId(User);
            _cartService.DeleteFromCartP(userId, productId);
        }

        [HttpPost]
        public IActionResult DeleteFromCartPOG(int pogId)
        {
            var userId = _userService.GetUserId(User);
            _cartpogService.DeleteFromCartPOG(userId, pogId);
            return RedirectToAction("CartPOGroduct");
        }
    }
}