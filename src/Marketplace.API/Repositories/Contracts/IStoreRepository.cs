using Marketplace.API.Models;

namespace Marketplace.API.Repositories.Contracts
{
    public interface IStoreRepository
    {
        Task<IEnumerable<Store>?> GetStores();
        Task<Store?> GetStore(ushort id);
        Task<Store?> CreateStore(Store store);
        Task<Store?> UpdateStore(Store store);
        Task<Store?> DeleteStore(ushort id);
    }
}