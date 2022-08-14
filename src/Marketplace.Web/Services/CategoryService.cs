using System.Text.Json;
using Marketplace.Web.Models;
using Marketplace.Web.Services.Contracts;

namespace Marketplace.Web.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly JsonSerializerOptions serializerOptions;
        private readonly HttpClient httpClient;

        public CategoryService(IHttpClientFactory httpClientFactory)
        {
            httpClient = httpClientFactory.CreateClient("Marketplace.API");
            
            serializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<IEnumerable<Category>?> GetCategories()
        {
            IEnumerable<Category>? categories = null;

            using (var response = await httpClient.GetAsync("api/categories"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStreamAsync();

                    categories = await JsonSerializer.DeserializeAsync<IEnumerable<Category>>(data, serializerOptions);
                }
            }

            return categories;
        }
    }
}