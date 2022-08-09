using Marketplace.API.Models;

namespace Marketplace.API.Repositories.Contracts
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>?> GetCategories(bool includeParent = false);
        Task<Category?> GetCategory(ushort id, bool includeParent = false);
        Task<Category?> CreateCategory(Category category);
        Task<Category?> UpdateCategory(Category category);
        Task<Category?> DeleteCategory(ushort id);
    }
}