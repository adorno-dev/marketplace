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

        public Guid UserId { get => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? ""); }
        
        public CartsController(ICartService cartService)
        {
            this.cartService = cartService;
        }

        public async Task<IActionResult> Index()
        {
            var cart = await cartService.GetCart(UserId);

            if (cart is null)
                cart = new Cart { UserId = UserId, Items = Array.Empty<CartItem>() };
            
            return View(cart);
        }

        [Route("add-item/{productId}")]
        [ActionName("add-item")]
        public async Task<IActionResult> AddItemToCart(Guid productId)
        {
            var request = new AddCartItemRequest { UserId = UserId, ProductId = productId };

            await cartService.AddCartItem(request);

            return RedirectToAction(nameof(Index));
        }

        [Route("delete-item/{cartItemId}")]
        [ActionName("delete-item")]
        public async Task<IActionResult> DeleteItemFromCart(Guid cartItemId)
        {
            var request = new DeleteCartItemRequest { UserId = UserId, CartItemId = cartItemId };

             await cartService.DeleteCartItem(request);

            return RedirectToAction(nameof(Index));
        }

        [Route("checkout")]
        [ActionName("checkout")]
        public async Task<IActionResult> Checkout()
        {
            this.ViewBag.Cart = await cartService.GetCart(UserId);

            return View();
        }

        [HttpPost]
        [Route("checkout")]
        [ActionName("checkout")]
        public async Task<IActionResult> Checkout(PlaceOrderRequest request)
        {
            if (ModelState.IsValid)
            {
                await Task.CompletedTask;

                return RedirectToAction("placeorder");
            }

            this.ViewBag.Cart = await cartService.GetCart(UserId);

            return View(request);
        }

        [Route("placeorder")]
        [ActionName("placeorder")]
        public async Task<IActionResult> PlaceOrder()
        {
            await Task.CompletedTask;

            return View("Placeorder", Random.Shared.Next(1000, 9999).ToString("D6"));
        }
    }
}