using Marketplace.Web.Contracts.Requests;
using Marketplace.Web.Models;

namespace Marketplace.Web.Services.Contracts
{
    public interface ICartService
    {
        Task<Cart?> GetCart(Guid id);
        Task<bool> DeleteCartItem(DeleteCartItemRequest request);
        Task<bool> AddCartItem(AddCartItemRequest request);
    }
}