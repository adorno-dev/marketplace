using Dapper;
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
            var cart = await context.Carts
                .Include("Items")
                .FirstOrDefaultAsync(c => c.UserId.Equals(userId));

            if (cart is null)
            {
                cart = new Cart { Id = Guid.NewGuid(), UserId = userId };
                cart.Items = new List<CartItem>();
                cart.Items.Append(item);

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
            var cartItem = await context.CartItems
                    .Include("Cart")
                    .Include("Cart.Items")
                    .FirstOrDefaultAsync(c => c.Id.Equals(item.Id));

            if (cartItem is not null)
            {
                if (cartItem.Cart?.Items?.Count() == 1)
                    context.Carts.Remove(cartItem.Cart);
                else
                    context.CartItems.Remove(cartItem);
                
                return await context.SaveChangesAsync() > 0;
            }

            return false;
        }

        public async Task<Cart?> GetCart(Guid userId)
        {
            //return await context.Carts
            //    .Include("Items")
            //    .Include("Items.Product")
            //    .AsNoTracking()
            //    .OrderBy(o => o.Id)
            //    .FirstOrDefaultAsync(c => c.UserId.Equals(userId));

            return await context.Carts
                .Include("Items")
                .Include("Items.Product")
                .Select(s => new Cart
                {
                    Id = s.Id,
                    UserId = s.UserId,
                    Items = s.Items != null ?
                        new List<CartItem>(s.Items.Select(si => 
                            new CartItem 
                            {
                                Id = si.Id,
                                ProductId = si.ProductId,
                                Quantity = si.Quantity,
                                Product = si.Product != null ?
                                    new Product 
                                    { 
                                        Id = si.Product.Id, 
                                        Price = si.Product.Price, 
                                        Name = si.Product.Name,
                                    } : null
                            })) : null
                })
                .OrderBy(o => o.Id)
                .FirstOrDefaultAsync(c => c.UserId.Equals(userId));

            // var items = await context.Database.GetDbConnection().QueryMultipleAsync(@$"
            //     SELECT
            //         c.Id,
            //         c.UserId,
            //         ci.Id,
            //         ci.ProductId,
            //         p.Name [Description],
            //         ci.Quantity,
            //         p.Price
            //     FROM Carts c
            //     INNER JOIN CartItems ci ON ci.CartId = c.Id
            //     INNER JOIN Products p ON ci.ProductId = p.Id
            //     WHERE c.UserId = @userId
            //     ORDER BY ci.Id
            // ", new { userId });

            // var cart = items.Read<Cart, CartItem, Product, Cart>((cart, item, product) => {
            //     item.Product = product;
            //     if (cart.Items == null)
            //         cart.Items = new List<CartItem>();
            //     cart.Items.Add(item);
            //     return cart;
            // }, splitOn: "UserId,ProductId").SingleOrDefault();

            // return cart;
        }

        public async Task<CartItem?> GetCartItem(Guid cartItemId)
        {
            return await context.CartItems
                .Include("Cart")
                .Include("Cart.User")
                .AsNoTracking()
                .OrderBy(o => o.Id)
                .FirstOrDefaultAsync(c => c.Id.Equals(cartItemId));
        }
    }
}