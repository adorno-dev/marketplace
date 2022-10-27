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

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> PlaceOrder()
        {
            return Ok();
        }
    }
}