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
        private readonly IStoreService storeService;

        private Guid userId { get => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? ""); }

        public OrdersController(ICartService cartService, IOrderService orderService, IStoreService storeService)
        {
            this.cartService = cartService;
            this.orderService = orderService;
            this.storeService = storeService;
        }

        [HttpGet]
        public async Task<ActionResult> GetOrders()
        {
            var storeId = await storeService.GetStoreIdByUserId(userId);

            if (storeId is null)
                return BadRequest("You don't have a store.");

            return Ok(await orderService.GetOrders(storeId.Value));
        }


        [HttpPost]
        public async Task<ActionResult> PlaceOrder()
        {
            var orderId = await orderService.PlaceOrder(userId);

            return orderId is not null ?
                Ok(orderId) :
                BadRequest();
        }
    }
}