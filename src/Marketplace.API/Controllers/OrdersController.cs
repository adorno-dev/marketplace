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
        private readonly IOrderService orderService;

        private Guid userId { get => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? ""); }

        public OrdersController(ICartService cartService, IOrderService orderService)
        {
            this.cartService = cartService;
            this.orderService = orderService;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> PlaceOrder()
        {
            return await orderService.PlaceOrder(userId) ?
                Ok():
                BadRequest();
        }
    }
}