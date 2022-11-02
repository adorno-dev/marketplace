using Marketplace.API.Models;

namespace Marketplace.API.Repositories.Contracts
{
    public interface IOrderRepository
    {
        Task<IList<OrderItem>> GetOrders(Guid storeId);
        Task<bool> PlaceOrder(Order order);
    }
}