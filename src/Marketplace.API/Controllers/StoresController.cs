using System.Security.Claims;
using Marketplace.API.Contracts.Requests;
using Marketplace.API.Contracts.Responses;
using Marketplace.API.Services.Contracts;
using Marketplace.API.Utils.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.API.Controllers
{
    [Authorize]
    [Route("api/stores")]
    [ApiController]
    public sealed class StoresController : ControllerBase
    {
        private readonly IStoreService service;

        public Guid UserId { get => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? ""); }

        public StoresController(IStoreService service) => this.service = service;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StoreResponse>>> GetStores()
        {
            var categories = await service.GetStores();

            return Ok(categories);
        }

        [AllowAnonymous]
        [HttpGet("pages/{skip:int?}/{take:int?}")]
        public async Task<ActionResult<IPagination<FavoriteResponse>>> GetStores(int skip = 1, int take = 12)
        {
            var stores = await service.GetStoresPaginated(skip, take);

            if (stores is null)
                return NotFound();

            return Ok(stores);
        } 

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<StoreResponse?>> GetStore(Guid id)
        {
            var category = await service.GetStore(id);

            if (category is null)
                return NotFound();
            
            return Ok(category);
        }

        [HttpGet("user")]
        public async Task<ActionResult<StoreResponse?>> GetStoreByUserId()
        {
            var category = await service.GetStoreByUserId(UserId);

            if (category is null)
                return NotFound();
            
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStore([FromForm] CreateStoreRequest request)
        {
            if (ModelState.IsValid)                
            {
                request.UserId = UserId;
                
                return await service.CreateStore(request) ?
                    Ok():
                    BadRequest();
            }

            return BadRequest(ModelState.Values);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateStore([FromBody] UpdateStoreRequest request)
        {
            if (ModelState.IsValid)                
            {
                return await service.UpdateStore(request) ?
                    Ok():
                    BadRequest();
            }

            return BadRequest(ModelState.Values);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStore(Guid id)
        {
            return await service.DeleteStore(id) ?
                Ok():
                NotFound();
        }
    }
}