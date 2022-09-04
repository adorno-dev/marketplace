using System;
using System.Security.Claims;
using Marketplace.Web.Contracts.Requests;
using Marketplace.Web.Models;
using Marketplace.Web.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Web.Controllers
{
    [Route("carts")]
    [Authorize]
    public class CartsController : Controller
    {
        private readonly ICartService cartService;

        public CartsController(ICartService cartService)
        {
            this.cartService = cartService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)); 

            var cart = await cartService.GetCart(userId);

            if (cart is null)
                cart = new Cart { UserId = userId, Items = Array.Empty<CartItem>() };
            
            return View(cart);
        }

        [Route("add-item/{productId}")]
        [ActionName("add-item")]
        public async Task<IActionResult> AddItemToCart(Guid productId)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)); 

            var request = new AddCartItemRequest { UserId = userId, ProductId = productId };

            await cartService.AddCartItem(request);

            return RedirectToAction(nameof(Index));
        }

        [Route("delete-item/{cartItemId}")]
        [ActionName("delete-item")]
        public async Task<IActionResult> DeleteItemFromCart(Guid cartItemId)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var request = new DeleteCartItemRequest { UserId = userId, CartItemId = cartItemId };

             await cartService.DeleteCartItem(request);

            return RedirectToAction(nameof(Index));
        }

        [Route("checkout")]
        [ActionName("checkout")]
        public async Task<IActionResult> Checkout()
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var checkout = new Checkout();
            checkout.BillingInfo = new BillingInfo();
            checkout.Cart = await cartService.GetCart(userId);

            return View(checkout);
        }
    }
}