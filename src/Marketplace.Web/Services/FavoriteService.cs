using System.Text.Json;
using Marketplace.Web.Models;
using Marketplace.Web.Services.Contracts;
using Marketplace.Web.Utils;
using Marketplace.Web.Utils.Contracts;

namespace Marketplace.Web.Services
{
    public class FavoriteService : ApiBaseService, IFavoriteService
    {
        public FavoriteService(IHttpClientFactory httpClientFactory) : base(httpClientFactory) {}

        public async Task<IPagination<Favorite>?> GetFavoritesPaginated(int? page = 1, int? size = null)
        {
            Pagination<Favorite>? favorites = null;

            using (var response = await httpClient.GetAsync($"api/favorites/pages/{page}" + (size.HasValue ? $"/{size}": "")))
            {
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStreamAsync();

                    favorites = await JsonSerializer.DeserializeAsync<Pagination<Favorite>>(data, serializerOptions);
                }
            }

            return favorites;
        }

        public async Task<bool> Favorite(Guid productId)
        {
            using (var response = await httpClient.PostAsync($"api/products/favorite/{productId}", null))
                return response.IsSuccessStatusCode;
        }

        public async Task<bool> UnFavorite(Guid productId)
        {
            using (var response = await httpClient.PostAsync($"api/products/unfavorite/{productId}", null))
                return response.IsSuccessStatusCode;
        }
    }
}