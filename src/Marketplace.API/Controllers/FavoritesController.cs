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

        public Guid UserId { get => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)); }

        public FavoritesController(IFavoriteService favoriteService)
        {
            this.favoriteService = favoriteService;
        }

        [HttpGet("pages/{skip:int?}/{take:int?}")]
        public async Task<ActionResult<IPagination<FavoriteResponse>>> GetFavorites(int skip = 1, int take = 12)
        {
            var favorites = await favoriteService.GetFavoritesPaginated(UserId, skip, take);

            if (favorites is null)
                return NotFound();

            return Ok(favorites);
        } 
    }
}