using Marketplace.Web.Models;
using Marketplace.Web.Utils.Contracts;

namespace Marketplace.Web.Services.Contracts
{
    public interface IFavoriteService
    {
        Task<IPagination<Favorite>?> GetFavoritesPaginated(int? page = 1, int? size = null);
        Task<bool> Favorite (Guid productId);
        Task<bool> UnFavorite (Guid productId);
    }
}