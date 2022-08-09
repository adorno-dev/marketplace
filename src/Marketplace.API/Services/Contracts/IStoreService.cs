using Marketplace.API.Contracts.Requests;
using Marketplace.API.Contracts.Responses;

namespace Marketplace.API.Services.Contracts
{
    public interface IStoreService
    {
        Task<IEnumerable<StoreResponse>?> GetStores();
        Task<StoreResponse?> GetStore(ushort id);
        Task<StoreResponse?> CreateStore(CreateStoreRequest request);
        Task<StoreResponse?> UpdateStore(UpdateStoreRequest request);
        Task<StoreResponse?> DeleteStore(ushort id);
    }
}