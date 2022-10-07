using Marketplace.API.Models;
using Marketplace.API.Utils.Contracts;

namespace Marketplace.API.Repositories.Contracts
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>?> GetProducts(Guid userId);
        Task<IPagination<Product>?> GetProductsPaginated(Guid userId, int skip, int take, bool includeParent = false);
        Task<Product?> GetProduct(Guid userId, Guid id);
        Task<Guid?> CreateProduct(Product product);
        Task<bool> UpdateProduct(Product product);
        Task<bool> DeleteProduct(Guid id);        
    }
}