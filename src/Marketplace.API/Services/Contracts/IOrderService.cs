using Marketplace.API.Models;

namespace Marketplace.API.Services.Contracts
{
    public interface IOrderService
    {
        Task<bool> PlaceOrder(Guid userId);        
    }
}