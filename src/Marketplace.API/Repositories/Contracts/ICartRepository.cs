using Marketplace.API.Models;

namespace Marketplace.API.Repositories.Contracts
{
    public interface ICartRepository
    {
        Task<Cart?> GetCartPaginated(Guid userId, int skip, int take, bool includeParent = false);
        Task<Cart?> GetCart(Guid userId);
        Task<CartItem?> GetCartItem(Guid cartItemId);
        Task<bool> DeleteCartItem(Guid userId, CartItem item);
        Task<bool> AddCartItem(Guid userId, CartItem item);
        Task<bool> UpdateCartItems(IEnumerable<CartItem> items);
    }
}