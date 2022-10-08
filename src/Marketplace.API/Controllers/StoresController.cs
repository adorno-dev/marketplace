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
            var stores = await service.GetStores();

            return Ok(stores);
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
            var store = await service.GetStore(id);

            if (store is null)
                return NotFound();
            
            return Ok(store);
        }

        [HttpGet("user")]
        public async Task<ActionResult<StoreResponse?>> GetStoreByUserId()
        {
            var store = await service.GetStoreByUserId(UserId);

            if (store is null)
                return NotFound();
            
            return Ok(store);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStore([FromForm] CreateStoreRequest request)
        {
            if (ModelState.IsValid)                
            {
                Guid? storeId = null;

                request.UserId = UserId;

                storeId = await service.CreateStore(request);

                if (storeId is null)
                    return BadRequest();
                
                // request.Banner == 700x100 (1MB) (jpg, png or gif)
                // request.Logo == 45x45 (jpg, png or gif)

                if (! await service.SaveStoreImages(storeId.Value, request.Logo, request.Banner))
                    return BadRequest();

                return Ok();
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