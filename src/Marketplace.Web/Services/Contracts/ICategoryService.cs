using Marketplace.Web.Contracts.Requests;
using Marketplace.Web.Models;
using Marketplace.Web.Utils.Contracts;

namespace Marketplace.Web.Services.Contracts
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>?> GetCategories();
        Task<IPagination<Category>?> GetCategoriesPaginated(int? page = 1);
        Task<Category?> GetCategory(ushort id);
        Task<bool> CreateCategory(CreateCategoryRequest request);
        Task<bool> UpdateCategory(UpdateCategoryRequest request);
        Task<bool> DeleteCategory(ushort id); 
    }
}