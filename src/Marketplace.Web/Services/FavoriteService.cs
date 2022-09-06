using Marketplace.Web.Services.Contracts;

namespace Marketplace.Web.Services
{
    public class FavoriteService : ApiBaseService, IFavoriteService
    {
        public FavoriteService(IHttpClientFactory httpClientFactory) : base(httpClientFactory) {}

        public async Task<bool> Favorite(Guid productId)
        {
            using (var response = await Api.PostAsync($"api/products/favorite/{productId}", null))
                return response.IsSuccessStatusCode;
        }

        public async Task<bool> UnFavorite(Guid productId)
        {
            using (var response = await Api.PostAsync($"api/products/unfavorite/{productId}", null))
                return response.IsSuccessStatusCode;
        }
    }
}