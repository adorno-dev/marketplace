using Marketplace.API.Contracts.Requests;
using Marketplace.API.Contracts.Responses;

namespace Marketplace.API.Services.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResponse>?> GetProducts();
        Task<ProductResponse?> GetProduct(Guid id);
        Task<bool> CreateProduct(CreateProductRequest request);
        Task<bool> UpdateProduct(UpdateProductRequest request);
        Task<bool> DeleteProduct(Guid id);
    }
}