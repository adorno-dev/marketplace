using Marketplace.API.Models;

namespace Marketplace.API.Repositories.Contracts
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>?> GetProducts();
        Task<Product?> GetProduct(Guid id);
        Task<bool> CreateProduct(Product product);
        Task<bool> UpdateProduct(Product product);
        Task<bool> DeleteProduct(Guid id);        
    }
}