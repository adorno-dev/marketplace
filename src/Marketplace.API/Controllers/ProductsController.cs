using Marketplace.API.Contracts.Requests;
using Marketplace.API.Contracts.Responses;
using Marketplace.API.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.API.Controllers
{
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
                var response = await service.CreateProduct(request);
                
                return Ok(response);
            }

            return BadRequest(ModelState.Values);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductRequest request)
        {
            if (ModelState.IsValid)                
            {
                var response = await service.UpdateProduct(request);
                
                return response is not null ?
                    Ok(response):
                    NotFound();
            }

            return BadRequest(ModelState.Values);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var response = await service.DeleteProduct(id);

            if (response is null)
                return NotFound();
            
            return Ok(response);
        }
    }
}