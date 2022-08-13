using Marketplace.API.Models;

namespace Marketplace.API.Repositories.Contracts
{
    public interface IStoreRepository
    {
        Task<IEnumerable<Store>?> GetStores();
        Task<Store?> GetStore(Guid id);
        Task<bool> CreateStore(Store store);
        Task<bool> UpdateStore(Store store);
        Task<bool> DeleteStore(Guid id);
    }
}