using Marketplace.API.Models;
using Marketplace.API.Utils.Contracts;

namespace Marketplace.API.Repositories.Contracts
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>?> GetProducts();
        Task<IPagination<Product>?> GetProductsPaginated(int skip, int take, bool includeParent = false);
        Task<Product?> GetProduct(Guid id);
        Task<bool> CreateProduct(Product product);
        Task<bool> UpdateProduct(Product product);
        Task<bool> DeleteProduct(Guid id);        
    }
}