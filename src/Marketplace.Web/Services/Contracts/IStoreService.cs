using Marketplace.Web.Contracts.Requests;
using Marketplace.Web.Models;

namespace Marketplace.Web.Services.Contracts
{
    public interface IStoreService
    {
        Task<Store?> GetStoreByUserId(Guid userId);
        Task<bool> Save(SaveStoreRequest request);
    }
}