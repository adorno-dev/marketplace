using Marketplace.API.Contracts.Requests;
using Marketplace.API.Contracts.Responses;
using Marketplace.API.Utils.Contracts;

namespace Marketplace.API.Services.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResponse>?> GetProducts();
        Task<IPagination<ProductResponse>?> GetProductsPaginated(int skip, int take, bool includeParent = false);
        Task<ProductResponse?> GetProduct(Guid id);
        Task<bool> CreateProduct(CreateProductRequest request);
        Task<bool> UpdateProduct(UpdateProductRequest request);
        Task<bool> DeleteProduct(Guid id);
    }
}