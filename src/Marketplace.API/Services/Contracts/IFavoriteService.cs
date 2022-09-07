using Marketplace.API.Contracts.Responses;
using Marketplace.API.Utils.Contracts;

namespace Marketplace.API.Services.Contracts
{
    public interface IFavoriteService
    {
        Task<IEnumerable<FavoriteResponse>?> GetFavorites(Guid userId);
        Task<IPagination<FavoriteResponse>?> GetFavoritesPaginated(Guid userId, int skip, int take);
        Task<FavoriteResponse?> GetFavorite(Guid userId, Guid productId);
        Task<bool> Favorite(Guid userId, Guid productId);
        Task<bool> Unfavorite(Guid userId, Guid productId);  
    }
}