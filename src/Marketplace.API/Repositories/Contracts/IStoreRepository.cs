using Marketplace.API.Models;
using Marketplace.API.Utils.Contracts;

namespace Marketplace.API.Repositories.Contracts
{
    public interface IStoreRepository
    {
        Task<IEnumerable<Store>?> GetStores();
        Task<Store?> GetStore(Guid id, int skip, int take);
        Task<IPagination<Store>?> GetStoresPaginated(int skip, int take);
        Task<Store?> GetStoreByUserId(Guid userId);
        Task<Guid?> GetStoreIdByUserId(Guid userId);
        Task<Guid?> CreateStore(Store store);
        Task<bool> UpdateStore(Store store);
        Task<Store?> DeleteStore(Guid id);
    }
}