using Marketplace.API.Models;

namespace Marketplace.API.Repositories.Contracts
{
    public interface IOrderRepository
    {
        Task<bool> PlaceOrder(Order order);
    }
}