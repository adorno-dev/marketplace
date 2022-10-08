using Marketplace.API.Contracts.Requests;
using Marketplace.API.Contracts.Responses;
using Marketplace.API.Utils.Contracts;

namespace Marketplace.API.Services.Contracts
{
    public interface IStoreService
    {
        Task<IEnumerable<StoreResponse>?> GetStores();
        Task<IPagination<StoreResponse>?> GetStoresPaginated(int skip, int take);
        Task<StoreResponse?> GetStore(Guid id);
        Task<StoreResponse?> GetStoreByUserId(Guid userId);
        Task<Guid?> GetStoreIdByUserId(Guid userId);
        Task<Guid?> CreateStore(CreateStoreRequest request);
        Task<bool> UpdateStore(UpdateStoreRequest request);
        Task<bool> DeleteStore(Guid id);

        Task<bool> SaveStoreImages(Guid id, IFormFile? logo, IFormFile? banner);
    }
}