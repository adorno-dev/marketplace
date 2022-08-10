using Marketplace.API.Contracts.Requests;
using Marketplace.API.Contracts.Responses;

namespace Marketplace.API.Services.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResponse>?> GetProducts();
        Task<ProductResponse?> GetProduct(Guid id);
        Task<ProductResponse?> CreateProduct(CreateProductRequest request);
        Task<ProductResponse?> UpdateProduct(UpdateProductRequest request);
        Task<ProductResponse?> DeleteProduct(Guid id);
    }
}