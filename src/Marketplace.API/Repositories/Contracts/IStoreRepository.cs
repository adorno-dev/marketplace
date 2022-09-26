using Marketplace.API.Models;
using Marketplace.API.Utils.Contracts;

namespace Marketplace.API.Repositories.Contracts
{
    public interface IStoreRepository
    {
        Task<IEnumerable<Store>?> GetStores();
        Task<Store?> GetStore(Guid id);
        Task<IPagination<Store>?> GetStoresPaginated(int skip, int take);
        Task<Store?> GetStoreByUserId(Guid userId);
        Task<Guid?> GetStoreIdByUserId(Guid userId);
        Task<bool> CreateStore(Store store);
        Task<bool> UpdateStore(Store store);
        Task<bool> DeleteStore(Guid id);
    }
}