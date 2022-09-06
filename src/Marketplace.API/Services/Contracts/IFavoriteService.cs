namespace Marketplace.API.Services.Contracts
{
    public interface IFavoriteService
    {
        Task<bool> Favorite(Guid userId, Guid productId);
        Task<bool> Unfavorite(Guid userId, Guid productId);  
    }
}