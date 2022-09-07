using AutoMapper;
using Marketplace.API.Contracts.Responses;
using Marketplace.API.Models;
using Marketplace.API.Repositories.Contracts;
using Marketplace.API.Services.Contracts;
using Marketplace.API.Utils;
using Marketplace.API.Utils.Contracts;

namespace Marketplace.API.Services
{
    public class FavoriteService : IFavoriteService
    {
        private IFavoriteRepository favoriteRepository;
        private IMapper mapper;

        public FavoriteService(IFavoriteRepository favoriteRepository, IMapper mapper)
        {
            this.favoriteRepository = favoriteRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<FavoriteResponse>?> GetFavorites(Guid userId)
        {
            var favorites = await favoriteRepository.GetFavorites(userId);

            return mapper.Map<IEnumerable<FavoriteResponse>?>(favorites);
        }

        public async Task<IPagination<FavoriteResponse>?> GetFavoritesPaginated(Guid userId, int skip, int take)
        {
            var favorites = await favoriteRepository.GetFavoritesPaginated(userId, skip, take);

            return mapper.Map<Pagination<FavoriteResponse>?>(favorites);
        }

        public async Task<FavoriteResponse?> GetFavorite(Guid userId, Guid productId)
        {
            var favorite = await favoriteRepository.GetFavorite(userId, productId);

            return mapper.Map<FavoriteResponse?>(favorite);
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