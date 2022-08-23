using System.Text.Json;
using Marketplace.Web.Models;
using Marketplace.Web.Services.Contracts;

namespace Marketplace.Web.Services
{
    public class ProductService : IProductService
    {
        private readonly JsonSerializerOptions serializerOptions;
        private readonly HttpClient httpClient;

        public ProductService(IHttpClientFactory httpClientFactory)
        {
            httpClient = httpClientFactory.CreateClient("Marketplace.API");

            serializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<IEnumerable<Product>?> GetProducts()
        {
            IEnumerable<Product>? products = null;

            using (var response = await httpClient.GetAsync("api/products"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStreamAsync();

                    products = await JsonSerializer.DeserializeAsync<IEnumerable<Product>>(data, serializerOptions);
                }
            }

            return products;
        }

        public async Task<Product?> GetProduct(Guid id)
        {
            Product? product = null;

            using (var response = await httpClient.GetAsync($"api/products/{id}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStreamAsync();

                    product = await JsonSerializer.DeserializeAsync<Product>(data, serializerOptions);
                }
            }

            return product;
        }
    }
}