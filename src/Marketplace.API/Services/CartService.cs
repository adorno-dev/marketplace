using AutoMapper;
using Marketplace.API.Contracts.Requests;
using Marketplace.API.Contracts.Responses;
using Marketplace.API.Models;
using Marketplace.API.Repositories.Contracts;
using Marketplace.API.Services.Contracts;

namespace Marketplace.API.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository cartRepository;
        private readonly IMapper mapper;

        public CartService(ICartRepository cartRepository, IMapper mapper)
        {
            this.cartRepository = cartRepository;
            this.mapper = mapper;
        }

        public async Task<bool> AddCartItem(AddCartItemRequest request)
        {
            var cartItem = new CartItem { ProductId = request.ProductId, Quantity = 1 };

            return await cartRepository.AddCartItem(request.UserId, cartItem);
        }

        public async Task<bool> DeleteCartItem(DeleteCartItemRequest request)
        {
            var cartItem = await cartRepository.GetCartItem(request.CartItemId);

            if (cartItem is null)
                return false;
            else if (cartItem.Cart is not null && cartItem.Cart.UserId.Equals(request.UserId))
            {
                return await cartRepository.DeleteCartItem(request.UserId, cartItem);
            }

            return false;
        }

        public async Task<CartPaginatedResponse?> GetCartPaginated(Guid userId, int skip, int take)
        {
            var cart = await cartRepository.GetCartPaginated(userId, skip, take);

            if (cart is not null && cart.Items is not null)
                foreach (var item in cart.Items)
                {
                    item.Price = item.Product?.Price * item.Quantity;
                    item.Description = item.Product?.Name;
                }

            return mapper.Map<CartPaginatedResponse?>(cart);
        }

        public async Task<CartResponse?> GetCart(Guid userId)
        {
            var cart = await cartRepository.GetCart(userId);

            if (cart is not null && cart.Items is not null)
                foreach (var item in cart.Items)
                {
                    item.Price = item.Product?.Price * item.Quantity;
                    item.Description = item.Product?.Name;
                }

            return mapper.Map<CartResponse?>(cart);
        }

        public async Task<bool> Checkout(CheckoutRequest request)
        {
            var cart = await cartRepository.GetCart(request.UserId);
            
            if (cart?.Items is not null)
            {
                foreach (var item in cart.Items)
                {
                    item.CartId  = cart.Id;
                    item.Quantity = request.Quantity[Array.IndexOf(request.ProductId, item.ProductId)];
                }
                
                return await cartRepository.UpdateCartItems(cart.Items);
            }

            return false;
        }
    }
}