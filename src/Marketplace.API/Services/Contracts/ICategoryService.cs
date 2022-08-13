using Marketplace.API.Contracts.Requests;
using Marketplace.API.Contracts.Responses;

namespace Marketplace.API.Services.Contracts
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryResponse>?> GetCategories(bool includeParent = false);
        Task<CategoryResponse?> GetCategory(ushort id, bool includeParent = false);
        Task<bool> CreateCategory(CreateCategoryRequest request);
        Task<bool> UpdateCategory(UpdateCategoryRequest request);
        Task<bool> DeleteCategory(ushort id);
    }
}