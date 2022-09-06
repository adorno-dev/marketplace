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
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService productService;
        private readonly IFavoriteService favoriteService;

        public Guid UserId { get => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)); }

        public ProductsController(IProductService service, IFavoriteService favoriteService)
        {
            this.productService = service;
            this.favoriteService = favoriteService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductResponse>>> GetProducts()
        {
            var categories = await productService.GetProducts();

            return Ok(categories);
        }

        [AllowAnonymous]
        [HttpGet("pages/{skip:int?}/{take:int?}")]
        public async Task<ActionResult<IPagination<ProductResponse>>> GetProductsPaginated(int skip = 1, int take = 10)
        {
            var products = await productService.GetProductsPaginated(skip, take, includeParent: true);

            if (products is null)
                return NotFound();

            return Ok(products);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResponse?>> GetProduct(Guid id)
        {
            var category = await productService.GetProduct(id);

            if (category is null)
                return NotFound();
            
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request)
        {
            if (ModelState.IsValid)                
            {
                return await productService.CreateProduct(request) ?
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
                return await productService.UpdateProduct(request) ?
                    Ok():
                    BadRequest();
            }

            return BadRequest(ModelState.Values);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            return await productService.DeleteProduct(id) ?
                Ok():
                NotFound();
        }

        [HttpPost("favorite/{productId}")]
        public async Task<IActionResult> Favorite(Guid productId)
        {
            return await favoriteService.Favorite(UserId, productId) ?
                Ok():
                Conflict();
        }

        [HttpPost("unfavorite/{productId}")]
        public async Task<IActionResult> Unfavorite(Guid productId)
        {
            return await favoriteService.Unfavorite(UserId, productId) ?
                Ok():
                NotFound();
        }
    }
}