using Marketplace.Web.Models;

namespace Marketplace.Web.Services.Contracts
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>?> GetCategories();
        Task<Category?> GetCategory(ushort id);
    }
}