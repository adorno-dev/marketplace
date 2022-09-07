using Marketplace.Web.Models;
using Marketplace.Web.Services.Contracts;
using Marketplace.Web.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Web.Controllers
{
    [Route("favorites")]
    [Authorize]
    public class FavoritesController : Controller
    {
        private IFavoriteService favoriteService;

        public FavoritesController(IFavoriteService favoriteService)
        {
            this.favoriteService = favoriteService;
        }

        public async Task<IActionResult> Index()
        {
            var favorites = await favoriteService.GetFavoritesPaginated();

            if (favorites is null)
                favorites = new Pagination<Favorite>() { Items = Array.Empty<Favorite>() };

            return View(favorites);
        }
    }
}