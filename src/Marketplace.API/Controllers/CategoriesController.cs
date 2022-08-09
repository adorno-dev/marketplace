using Marketplace.API.Contracts.Requests;
using Marketplace.API.Contracts.Responses;
using Marketplace.API.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService service;

        public CategoriesController(ICategoryService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IEnumerable<CategoryResponse>?> GetCategories() => await service.GetCategories();

        [HttpGet("{id}")]
        public async Task<CategoryResponse?> GetCategory(ushort id) => await service.GetCategory(id);

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequest request)
        {
            if (ModelState.IsValid)                
            {
                var response = await service.CreateCategory(request);
                
                return Ok(response);
            }

            return BadRequest(ModelState.Values);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryRequest request)
        {
            if (ModelState.IsValid)                
            {
                var response = await service.UpdateCategory(request);
                
                return Ok(response);
            }

            return BadRequest(ModelState.Values);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(ushort id)
        {
            var response = await service.DeleteCategory(id);

            if (response is null)
                return NotFound();
            
            return Ok(response);
        }
    }
}