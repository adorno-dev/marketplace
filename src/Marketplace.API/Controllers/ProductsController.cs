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
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService productService;
        private readonly IFavoriteService favoriteService;
        private readonly IStoreService storeService;

        public Guid UserId { get => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? ""); }

        public ProductsController(IProductService service, IFavoriteService favoriteService, IStoreService storeService)
        {
            this.productService = service;
            this.favoriteService = favoriteService;
            this.storeService = storeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductResponse>>> GetProducts()
        {
            Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid UserId);

            var categories = await productService.GetProducts(UserId);

            return Ok(categories);
        }

        [AllowAnonymous]
        [HttpGet("pages/{skip:int?}/{take:int?}")]
        public async Task<ActionResult<IPagination<ProductResponse>>> GetProductsPaginated(int skip = 1, int take = 20)
        {
            Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid UserId);

            var products = await productService.GetProductsPaginated(UserId, skip, take, includeParent: true);

            if (products?.Items is null)
                return NotFound();
            
            foreach (var item in products.Items)
                item.Screenshoot = item.GetScreenshoot();
            
            return Ok(products);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResponse?>> GetProduct(Guid id)
        {
            Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid UserId);

            var product = await productService.GetProduct(UserId, id);

            if (product is null)
                return NotFound();
            
            product.Screenshoots = product.GetScreenshoots();

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromForm] CreateProductRequest request)
        {
            if (ModelState.IsValid)                
            {
                Guid? productId = null;

                var storeId = await storeService.GetStoreIdByUserId(UserId);

                if (storeId is null || storeId.Equals(Guid.Empty))
                    return BadRequest("Store required.");
                
                request.StoreId = storeId.Value;

                productId = await productService.CreateProduct(request);
                
                if (productId is null)
                    return BadRequest();

                if (productId is not null && request.Screenshoots != null)
                    await productService.SaveProductScreenshoots(productId.Value, request.Screenshoots);
                
                return Ok();
            }

            return BadRequest(ModelState.Values);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromForm] UpdateProductRequest request)
        {
            if (ModelState.IsValid)                
            {
                if (request.Screenshoots != null)
                    await productService.SaveProductScreenshoots(request.Id, request.Screenshoots);

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