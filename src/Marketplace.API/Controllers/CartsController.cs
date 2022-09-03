using Marketplace.API.Contracts.Requests;
using Marketplace.API.Contracts.Responses;
using Marketplace.API.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly ICartService cartService;

        public CartsController(ICartService cartService)
        {
            this.cartService = cartService;
        }

        [HttpGet]
        [Route("{userId}")]
        public async Task<ActionResult<CartResponse>> GetCart(Guid userId)
        {
            var cart = await cartService.GetCart(userId);

            return cart is not null ?
                Ok(cart):
                NotFound();
        }

        [HttpPost]
        [Route("add-item")]
        public async Task<IActionResult> AddCartItem(AddCartItemRequest request)
        {
            if (request.UserId == Guid.Empty || request.ProductId == Guid.Empty)
                return BadRequest();

            await cartService.AddCartItem(request);

            return RedirectToAction("index", "home");
        }

        [HttpPost]
        [Route("remove-item")]
        public async Task<IActionResult> DeleteCartItem(DeleteCartItemRequest request)
        {
            if (request.UserId == Guid.Empty || request.CartItemId == Guid.Empty)
                return BadRequest();
            

            await cartService.DeleteCartItem(request);

            return RedirectToAction(nameof(GetCart), new { userId = request.UserId });
        }
    }
}