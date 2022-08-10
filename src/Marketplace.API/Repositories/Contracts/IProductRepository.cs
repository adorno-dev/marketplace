using Marketplace.API.Models;

namespace Marketplace.API.Repositories.Contracts
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>?> GetProducts();
        Task<Product?> GetProduct(Guid id);
        Task<Product?> CreateProduct(Product store);
        Task<Product?> UpdateProduct(Product store);
        Task<Product?> DeleteProduct(Guid id);        
    }
}