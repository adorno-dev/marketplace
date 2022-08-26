using System.Text.Json;
using Marketplace.Web.Contracts.Requests;
using Marketplace.Web.Models;
using Marketplace.Web.Services.Contracts;

namespace Marketplace.Web.Services
{
    public class StoreService : IStoreService
    {
        private readonly JsonSerializerOptions serializerOptions;
        private readonly HttpClient httpClient;

        public StoreService(IHttpClientFactory httpClientFactory)
        {
            httpClient = httpClientFactory.CreateClient("Marketplace.API");

            serializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<Store?> GetStoreByUserId(Guid userId)
        {
            Store? store = null;

            using (var response = await httpClient.GetAsync($"api/stores/user/{userId}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStreamAsync();

                    store = await JsonSerializer.DeserializeAsync<Store>(data, serializerOptions);
                }
            }

            return store;
        }

        public async Task<bool> Save(SaveStoreRequest request)
        {
            if (request.Id is null)
                using (var post = await httpClient.PostAsJsonAsync<SaveStoreRequest>("api/stores", request))
                    return post.IsSuccessStatusCode;
            else
                using (var post = await httpClient.PutAsJsonAsync<SaveStoreRequest>("api/stores", request))
                    return post.IsSuccessStatusCode;
        }
    }
}