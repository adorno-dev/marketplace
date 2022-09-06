using Marketplace.API.Models;
using Marketplace.API.Utils.Contracts;

namespace Marketplace.API.Repositories.Contracts
{
    public interface IFavoriteRepository
    {
        Task<IEnumerable<Favorite>?> GetFavorites(Guid userId);
        Task<IPagination<Favorite>?> GetFavoritesPaginated(Guid userId, int skip, int take);
        Task<Favorite?> GetFavorite(Guid userId, Guid productId);
        Task<bool> Favorite(Favorite favorite);
        Task<bool> Unfavorite(Favorite favorite);
    }
}