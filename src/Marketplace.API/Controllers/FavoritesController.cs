using System.Security.Claims;
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
    public class FavoritesController : ControllerBase
    {
        private readonly IFavoriteService favoriteService;
        private readonly IProductService productService;

        public Guid UserId { get => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)); }

        public FavoritesController(IFavoriteService favoriteService, IProductService productService)
        {
            this.favoriteService = favoriteService;
            this.productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<FavoriteResponse>> GetFavorites()
        {
            var favorites = await favoriteService.GetFavorites(UserId);

            if (favorites is null)
                return NotFound();
            
            return Ok(favorites);
        }

        [HttpGet("pages/{skip:int?}/{take:int?}")]
        public async Task<ActionResult<IPagination<FavoriteResponse>>> GetFavorites(int skip = 1, int take = 12)
        {
            var favorites = await favoriteService.GetFavoritesPaginated(UserId, skip, take);

            if (favorites?.Items is null)
                return NotFound();
            
            foreach (var item in favorites.Items)
                item.Screenshoot = await productService.GetScreenshot(item.Id);

            return Ok(favorites);
        } 
    }
}