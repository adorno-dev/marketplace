using System.Security.Claims;
using Marketplace.API.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.API.Controllers
{
    [Authorize]
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ICartService cartService;

        private Guid userId { get => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? ""); }

        public OrdersController(ICartService cartService)
        {
            this.cartService = cartService;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> PlaceOrder()
        {
            var cart = await cartService.GetCart(userId);

            

            return BadRequest();
        }
    }
}