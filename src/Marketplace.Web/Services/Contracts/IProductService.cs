using Marketplace.Web.Models;

namespace Marketplace.Web.Services.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<Product>?> GetProducts();
        Task<Product?> GetProduct(Guid id);
    }
}