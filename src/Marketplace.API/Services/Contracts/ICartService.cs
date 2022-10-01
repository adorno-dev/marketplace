using Marketplace.API.Contracts.Requests;
using Marketplace.API.Contracts.Responses;

namespace Marketplace.API.Services.Contracts
{
    public interface ICartService
    {
        Task<CartPaginatedResponse?> GetCartPaginated(Guid userId, int skip, int take);
        Task<CartResponse?> GetCart(Guid userId);
        Task<bool> DeleteCartItem(DeleteCartItemRequest request);
        Task<bool> AddCartItem(AddCartItemRequest request);
        Task<bool> Checkout(CheckoutRequest request);
    }
}