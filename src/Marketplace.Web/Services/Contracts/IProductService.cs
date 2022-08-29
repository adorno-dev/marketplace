using Marketplace.Web.Contracts.Requests;
using Marketplace.Web.Models;
using Marketplace.Web.Utils.Contracts;

namespace Marketplace.Web.Services.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<Product>?> GetProducts();
        Task<IPagination<Product>?> GetProductsPaginated(int? page = 1);
        Task<Product?> GetProduct(Guid id);
        Task<bool> CreateProduct(CreateProductRequest request);
        Task<bool> UpdateProduct(UpdateProductRequest request);
        Task<bool> DeleteProduct(Guid id); 
    }
}