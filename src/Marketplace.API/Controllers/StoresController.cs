using Marketplace.API.Contracts.Requests;
using Marketplace.API.Contracts.Responses;
using Marketplace.API.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoresController : ControllerBase
    {
        private readonly IStoreService service;

        public StoresController(IStoreService service) => this.service = service;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StoreResponse>>> GetStores()
        {
            var categories = await service.GetStores();

            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StoreResponse?>> GetStore(ushort id)
        {
            var category = await service.GetStore(id);

            if (category is null)
                return NotFound();
            
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStore([FromBody] CreateStoreRequest request)
        {
            if (ModelState.IsValid)                
            {
                var response = await service.CreateStore(request);
                
                return Ok(response);
            }

            return BadRequest(ModelState.Values);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateStore([FromBody] UpdateStoreRequest request)
        {
            if (ModelState.IsValid)                
            {
                var response = await service.UpdateStore(request);
                
                return response is not null ?
                    Ok(response):
                    NotFound();
            }

            return BadRequest(ModelState.Values);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStore(ushort id)
        {
            var response = await service.DeleteStore(id);

            if (response is null)
                return NotFound();
            
            return Ok(response);
        }
    }
}