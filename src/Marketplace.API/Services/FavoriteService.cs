using Marketplace.API.Models;
using Marketplace.API.Repositories.Contracts;
using Marketplace.API.Services.Contracts;

namespace Marketplace.API.Services
{
    public class FavoriteService : IFavoriteService
    {
        private IFavoriteRepository favoriteRepository;

        public FavoriteService(IFavoriteRepository favoriteRepository)
        {
            this.favoriteRepository = favoriteRepository;
        }

        public async Task<bool> Favorite(Guid userId, Guid productId)
        {
            var favorite = new Favorite { UserId = userId, ProductId = productId };

            return await favoriteRepository.Favorite(favorite);
        }

        public async Task<bool> Unfavorite(Guid userId, Guid productId)
        {
            var favorite = await favoriteRepository.GetFavorite(userId, productId);

            if (favorite is null)
                return false;

            return await favoriteRepository.Unfavorite(favorite);
        }
    }
}