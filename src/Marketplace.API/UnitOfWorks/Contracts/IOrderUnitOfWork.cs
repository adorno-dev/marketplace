using Marketplace.API.Models;

namespace Marketplace.API.UnitOfWorks.Contracts
{
    public interface IOrderUnitOfWork
    {
        Task Commit();
        Task Rollback();
        Task<IList<OrderItem>> GetOrders(Guid storeId);
        Task<Guid?> PlaceOrder(Guid userId);
    }
}