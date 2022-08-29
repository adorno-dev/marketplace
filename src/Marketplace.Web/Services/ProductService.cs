using System.Text.Json;
using Marketplace.Web.Contracts.Requests;
using Marketplace.Web.Models;
using Marketplace.Web.Services.Contracts;
using Marketplace.Web.Utils;
using Marketplace.Web.Utils.Contracts;

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

        public async Task<IPagination<Product>?> GetProductsPaginated(int? page = 1)
        {
            Pagination<Product>? products = null;

            using (var response = await httpClient.GetAsync($"api/products/pages/{page}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStreamAsync();

                    products = await JsonSerializer.DeserializeAsync<Pagination<Product>>(data, serializerOptions);
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

        public async Task<bool> CreateProduct(CreateProductRequest request)
        {
            using (var post = await httpClient.PostAsJsonAsync<CreateProductRequest>("api/products", request))
                return post.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateProduct(UpdateProductRequest request)
        {
            using (var put = await httpClient.PutAsJsonAsync<UpdateProductRequest>("api/products", request))
                return put.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteProduct(Guid id)
        {
            using (var delete = await httpClient.DeleteAsync($"api/products/{id}"))
                return delete.IsSuccessStatusCode;
        }
    }
}