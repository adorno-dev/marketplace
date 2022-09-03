using System.Text.Json;
using System.Text.Json.Serialization;
using Marketplace.Web.Contracts.Requests;
using Marketplace.Web.Models;
using Marketplace.Web.Services.Contracts;

namespace Marketplace.Web.Services
{
    public class CartService : ICartService
    {
        private readonly JsonSerializerOptions serializerOptions;
        private readonly HttpClient httpClient;

        public CartService(IHttpClientFactory httpClientFactory)
        {
            httpClient = httpClientFactory.CreateClient("Marketplace.API");
            
            serializerOptions = new JsonSerializerOptions 
            { 
                PropertyNameCaseInsensitive = true,
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull 
            };
        }

        public async Task<Cart?> GetCart(Guid id)
        {
            Cart? cart = null;

            using (var response = await httpClient.GetAsync($"api/carts/{id}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStreamAsync();

                    cart = await JsonSerializer.DeserializeAsync<Cart>(data, serializerOptions);
                }
            }

            return cart;
        }

        public async Task<bool> AddCartItem(AddCartItemRequest request)
        {
            using (var post = await httpClient.PostAsJsonAsync<AddCartItemRequest>("api/carts/add-item", request))
                return post.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteCartItem(DeleteCartItemRequest request)
        {
            using (var post = await httpClient.PostAsJsonAsync<DeleteCartItemRequest>("api/carts/remove-item", request))
                return post.IsSuccessStatusCode;
        }
    }
}