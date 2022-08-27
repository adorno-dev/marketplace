using Marketplace.Web.Contracts.Requests;
using Marketplace.Web.Models;

namespace Marketplace.Web.Services.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<Product>?> GetProducts();
        Task<Product?> GetProduct(Guid id);
        Task<bool> CreateProduct(CreateProductRequest request);
        Task<bool> UpdateProduct(UpdateProductRequest request);
        Task<bool> DeleteProduct(Guid id); 
    }
}