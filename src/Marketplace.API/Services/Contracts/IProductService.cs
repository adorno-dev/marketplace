using Marketplace.API.Contracts.Requests;
using Marketplace.API.Contracts.Responses;
using Marketplace.API.Utils.Contracts;

namespace Marketplace.API.Services.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResponse>?> GetProducts(Guid userId);
        Task<IPagination<ProductResponse>?> GetProductsPaginated(Guid userId, int skip, int take, bool includeParent = false);
        Task<IPagination<ProductResponse>?> GetStoreProductsPaginated(Guid userId, Guid storeId, int skip, int take, bool includeParent = false);
        Task<ProductResponse?> GetProduct(Guid userId, Guid id);
        Task<Guid?> CreateProduct(CreateProductRequest request);
        Task<bool> UpdateProduct(UpdateProductRequest request);
        Task<bool> DeleteProduct(Guid id);

        // Task<string[]?> GetScreenshoots(Guid id);
        // Task<string?> GetScreenshot(Guid id);

        Task SaveProductScreenshoots(Guid id, IFormFileCollection files);
    }
}