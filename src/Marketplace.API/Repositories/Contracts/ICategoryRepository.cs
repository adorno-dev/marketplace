using Marketplace.API.Models;

namespace Marketplace.API.Repositories.Contracts
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>?> GetCategories(bool includeParent = false);
        Task<IEnumerable<Category>?> GetCategories(params ushort[] ids);
        Task<Category?> GetCategory(ushort id, bool includeParent = false);
        Task<bool> CreateCategory(Category category);
        Task<bool> UpdateCategory(Category category);
        Task<bool> DeleteCategory(ushort id);
    }
}