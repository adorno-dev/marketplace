namespace Marketplace.Web.Services.Contracts
{
    public interface IFavoriteService
    {
        Task<bool> Favorite (Guid productId);
        Task<bool> UnFavorite (Guid productId);
    }
}