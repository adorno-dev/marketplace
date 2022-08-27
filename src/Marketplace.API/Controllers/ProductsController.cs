using Marketplace.API.Contracts.Requests;
using Marketplace.API.Contracts.Responses;
using Marketplace.API.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.API.Controllers
{
    // [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService service;

        public ProductsController(IProductService service) => this.service = service;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductResponse>>> GetProducts()
        {
            var categories = await service.GetProducts();

            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResponse?>> GetProduct(Guid id)
        {
            var category = await service.GetProduct(id);

            if (category is null)
                return NotFound();
            
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request)
        {
            if (ModelState.IsValid)                
            {
                return await service.CreateProduct(request) ?
                    Ok():
                    BadRequest();
            }

            return BadRequest(ModelState.Values);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductRequest request)
        {
            if (ModelState.IsValid)                
            {
                return await service.UpdateProduct(request) ?
                    Ok():
                    BadRequest();
            }

            return BadRequest(ModelState.Values);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            return await service.DeleteProduct(id) ?
                Ok():
                NotFound();
        }
    }
}