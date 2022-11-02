using Marketplace.API.Contracts.Responses;

namespace Marketplace.API.Services.Contracts
{
    public interface IOrderService
    {
        Task<IList<OrderItemResponse?>> GetOrders(Guid storeId);
        Task<bool> PlaceOrder(Guid userId);        
    }
}