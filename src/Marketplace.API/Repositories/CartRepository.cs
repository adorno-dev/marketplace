using Marketplace.API.Data;
using Marketplace.API.Models;
using Marketplace.API.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.API.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly DatabaseContext context;

        public CartRepository(DatabaseContext context)
        {
            this.context = context;
        }

        public async Task<bool> AddCartItem(Guid userId, CartItem item)
        {
            var cart = await context.Carts.Include("Items").FirstOrDefaultAsync(c => c.UserId.Equals(userId));

            if (cart is null)
            {
                cart = new Cart { Id = Guid.NewGuid(), UserId = userId };
                cart.Items = new List<CartItem>();
                cart.Items.Add(item);

                context.Carts.Add(cart);
                return await context.SaveChangesAsync() > 0;
            }
            else
            {
                item.CartId = cart.Id;
                context.CartItems.Add(item);
                return await context.SaveChangesAsync() > 0;
            }
        }

        public async Task<bool> DeleteCartItem(Guid userId, CartItem item)
        {
            var cartItem = await context.CartItems.Include("Cart").Include("Cart.Items").FirstOrDefaultAsync(c => c.Id.Equals(item.Id));

            if (cartItem is not null)
            {
                if (cartItem.Cart?.Items?.Count == 1)
                    context.Carts.Remove(cartItem.Cart);
                else
                    context.CartItems.Remove(cartItem);
                
                return await context.SaveChangesAsync() > 0;
            }

            return false;
        }

        public async Task<Cart?> GetCart(Guid userId)
        {
            return await context.Carts.Include("Items").Include("Items.Product").AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<CartItem?> GetCartItem(Guid cartItemId)
        {
            return await context.CartItems.Include("Cart").Include("Cart.User").FirstOrDefaultAsync(c => c.Id.Equals(cartItemId));
        }
    }
}