using System.Text.Json;
using System.Text.Json.Serialization;
using Marketplace.Web.Contracts.Requests;
using Marketplace.Web.Models;
using Marketplace.Web.Services.Contracts;
using Marketplace.Web.Utils;
using Marketplace.Web.Utils.Contracts;

namespace Marketplace.Web.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly JsonSerializerOptions serializerOptions;
        private readonly HttpClient httpClient;

        public CategoryService(IHttpClientFactory httpClientFactory)
        {
            httpClient = httpClientFactory.CreateClient("Marketplace.API");
            
            serializerOptions = new JsonSerializerOptions 
            { 
                PropertyNameCaseInsensitive = true,
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull 
            };
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

        public async Task<IPagination<Category>?> GetCategoriesPaginated(int? page = 1)
        {
            Pagination<Category>? categories = null;

            using (var response = await httpClient.GetAsync($"api/categories/pages/{page}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStreamAsync();

                    categories = await JsonSerializer.DeserializeAsync<Pagination<Category>>(data, serializerOptions);
                }
            }

            return categories;
        }

        public async Task<Category?> GetCategory(ushort id)
        {
            Category? category = null;

            using (var response = await httpClient.GetAsync($"api/categories/{id}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStreamAsync();

                    category = await JsonSerializer.DeserializeAsync<Category>(data, serializerOptions);
                }
            }

            return category;
        }

        public async Task<bool> CreateCategory(CreateCategoryRequest request)
        {
            using (var post = await httpClient.PostAsJsonAsync<CreateCategoryRequest>("api/categories", request))
                return post.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateCategory(UpdateCategoryRequest request)
        {
            using (var put = await httpClient.PutAsJsonAsync<UpdateCategoryRequest>("api/categories", request))
                return put.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteCategory(ushort id)
        {
            using (var delete = await httpClient.DeleteAsync($"api/categories/{id}"))
                return delete.IsSuccessStatusCode;
        }
    }
}