using Marketplace.API.Contracts.Requests;
using Marketplace.API.Contracts.Responses;
using Marketplace.API.Utils.Contracts;

namespace Marketplace.API.Services.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResponse>?> GetProducts(Guid userId);
        Task<IPagination<ProductResponse>?> GetProductsPaginated(Guid userId, int skip, int take, bool includeParent = false);
        Task<ProductResponse?> GetProduct(Guid userId, Guid id);
        Task<bool> CreateProduct(CreateProductRequest request);
        Task<bool> UpdateProduct(UpdateProductRequest request);
        Task<bool> DeleteProduct(Guid id);
    }
}