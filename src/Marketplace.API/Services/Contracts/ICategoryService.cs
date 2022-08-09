using Marketplace.API.Contracts.Requests;
using Marketplace.API.Contracts.Responses;

namespace Marketplace.API.Services.Contracts
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryResponse>?> GetCategories(bool includeParent = false);
        Task<CategoryResponse?> GetCategory(ushort id, bool includeParent = false);
        Task<CategoryResponse?> CreateCategory(CreateCategoryRequest request);
        Task<CategoryResponse?> UpdateCategory(UpdateCategoryRequest request);
        Task<CategoryResponse?> DeleteCategory(ushort id);
    }
}