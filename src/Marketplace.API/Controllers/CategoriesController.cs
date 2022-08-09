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
        public async Task<ActionResult<IEnumerable<CategoryResponse>>> GetCategories()
        {
            var categories = await service.GetCategories(true);

            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryResponse?>> GetCategory(ushort id)
        {
            var category = await service.GetCategory(id);

            if (category is null)
                return NotFound();
            
            return Ok();
        }

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