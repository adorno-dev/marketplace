using Marketplace.API.Contracts.Requests;
using Marketplace.API.Contracts.Responses;
using Marketplace.API.Services.Contracts;
using Marketplace.API.Utils.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.API.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService service;

        public CategoriesController(ICategoryService service) => this.service = service;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryResponse>>> GetCategories()
        {
            var categories = await service.GetCategories(includeParent: true);

            return Ok(categories);
        }

        [HttpGet("pages/{skip:int?}/{take:int?}")]
        public async Task<ActionResult<IPagination<CategoryResponse>>> GetCategoriesPaginated(int skip = 1, int take = 12)
        {
            var categories = await service.GetCategoriesPaginated(skip, take, includeParent: true);

            if (categories is null)
                return NotFound();

            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryResponse?>> GetCategory(ushort id)
        {
            var category = await service.GetCategory(id, true);

            if (category is null)
                return NotFound();
            
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequest request)
        {
            if (ModelState.IsValid)                
            {
                return await service.CreateCategory(request) ?
                    Ok():
                    BadRequest();
            }

            return BadRequest(ModelState.Values);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryRequest request)
        {
            if (ModelState.IsValid)                
            {
                return await service.UpdateCategory(request) ?
                    Ok():
                    BadRequest();
            }

            return BadRequest(ModelState.Values);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(ushort id)
        {
            return await service.DeleteCategory(id) ?
                Ok():
                NotFound();
        }
    }
}