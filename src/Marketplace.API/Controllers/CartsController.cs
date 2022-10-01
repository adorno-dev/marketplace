using System.Security.Claims;
using Marketplace.API.Contracts.Requests;
using Marketplace.API.Contracts.Responses;
using Marketplace.API.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly ICartService cartService;
        private readonly IProductService productService;
        
        public Guid UserId { get => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? ""); }

        public CartsController(ICartService cartService, IProductService productService)
        {
            this.cartService = cartService;
            this.productService = productService;
        }

        [HttpGet]
        [Route("{userId}")]
        public async Task<ActionResult<CartResponse>> GetCart(Guid userId)
        {
            var cart = await cartService.GetCart(UserId);

            if (cart is null || cart.Items is null)
                return BadRequest();

            foreach (var item in cart.Items)
                item.Screenshoot = await productService.GetScreenshot(item.ProductId);

            return Ok(cart);
        }

        [HttpGet]
        public async Task<ActionResult<CartResponse>> GetCart()
        {
            var cart = await cartService.GetCart(UserId);

            if (cart is null || cart.Items is null)
                return Ok(new CartResponse { Id = Guid.NewGuid(), UserId = UserId, Items = Array.Empty<CartItemResponse>() });

            foreach (var item in cart.Items)
                item.Screenshoot = await productService.GetScreenshot(item.ProductId);

            return Ok(cart);
        }

        [HttpGet("pages/{skip:int?}/{take:int?}")]
        public async Task<ActionResult<CartPaginatedResponse>> GetCartPaginated(int skip = 1, int take = 12)
        {
            var cart = await cartService.GetCartPaginated(UserId, skip, take);

            if (cart is null || cart.Items is null)
                return Ok(new CartResponse { Id = Guid.NewGuid(), UserId = UserId, Items = Array.Empty<CartItemResponse>() });

            foreach (var item in cart.Items)
                item.Screenshoot = await productService.GetScreenshot(item.ProductId);

            return Ok(cart);
        }

        [HttpPost]
        [Route("add-item")]
        public async Task<IActionResult> AddCartItem(AddCartItemRequest request)
        {
            bool success = false;

            request.UserId = UserId;

            if (ModelState.IsValid)
                success = await cartService.AddCartItem(request);

            return success ?
                Ok():
                BadRequest();
        }

        [HttpPost]
        [Route("remove-item")]
        public async Task<IActionResult> DeleteCartItem(DeleteCartItemRequest request)
        {
            bool success = false;

            request.UserId = UserId;

            if (ModelState.IsValid)
                success = await cartService.DeleteCartItem(request);

            return success ?
                Ok():
                BadRequest();
        }

        [HttpPost]
        [Route("checkout")]
        public async Task<IActionResult> Checkout(CheckoutRequest request)
        {
            await Task.CompletedTask;

            if (ModelState.IsValid)
            {
                request.UserId = UserId;

                return await cartService.Checkout(request) ?
                    Ok():
                    BadRequest();
            }

            return BadRequest();
        }
    }
}