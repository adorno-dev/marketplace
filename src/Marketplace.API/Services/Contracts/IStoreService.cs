using Marketplace.API.Contracts.Requests;
using Marketplace.API.Contracts.Responses;

namespace Marketplace.API.Services.Contracts
{
    public interface IStoreService
    {
        Task<IEnumerable<StoreResponse>?> GetStores();
        Task<StoreResponse?> GetStore(Guid id);
        Task<StoreResponse?> GetStoreByUserId(Guid userId);
        Task<bool> CreateStore(CreateStoreRequest request);
        Task<bool> UpdateStore(UpdateStoreRequest request);
        Task<bool> DeleteStore(Guid id);
    }
}