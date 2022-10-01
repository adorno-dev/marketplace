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
                item.CartId = cart.Id;
                item.Quantity = 1;
                
                context.Carts.Add(cart);
                context.CartItems.Add(item);

                return await context.SaveChangesAsync() > 0;
            }
            else if (cart.Items != null && ! cart.Items.Any(w => w.ProductId.Equals(item.ProductId)))
            {
                item.CartId = cart.Id;
                context.CartItems.Add(item);
                return await context.SaveChangesAsync() > 0;
            }

            return false;
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

        public async Task<Cart?> GetCartPaginated(Guid userId, int skip, int take, bool includeParent = false)
        {
            var cart = new Cart();

            cart.PageIndex = skip <= 0 ? 1 : skip;
            cart.PageSize = take;

            var items = await context.Database.GetDbConnection().QueryMultipleAsync(@$"
                SELECT COUNT(c.Id) TotalItems FROM Carts c INNER JOIN CartItems ci ON ci.CartId = c.Id WHERE c.UserId = @userId  
                SELECT
                    c.Id,
                    c.UserId,
                    ci.Id,
                    ci.ProductId,
                    ci.Quantity,
                    p.Id,
                    p.Name,
                    p.Price
                FROM Carts c
                INNER JOIN CartItems ci ON ci.CartId = c.Id
                INNER JOIN Products p ON ci.ProductId = p.Id
                WHERE c.UserId = @userId
                ORDER BY ci.Id
                OFFSET (@pageIndex - 1) * @pageSize ROWS
                FETCH NEXT @pageSize ROWS ONLY
            ", new { userId, pageIndex = cart.PageIndex, pageSize = cart.PageSize });

            cart.SetCount(items.Read<int>().Single());

            items.Read<Cart, CartItem, Product, Cart>((parent, item, product) => {

                if (cart.Items == null) {
                    cart.Items = new List<CartItem>();
                    cart.Id = parent.Id;
                    cart.UserId = parent.UserId;
                }

                item.Product = product;
                cart.Items.Add(item);

                return parent;                
            }, splitOn: "Id,Id");

            return cart;

        }

        public async Task<Cart?> GetCart(Guid userId)
        {
            var items = await context.Database.GetDbConnection().QueryMultipleAsync(@$"
                SELECT
                    c.Id,
                    c.UserId,
                    ci.Id,
                    ci.ProductId,
                    ci.Quantity,
                    p.Id,
                    p.Name,
                    p.Price
                FROM Carts c
                INNER JOIN CartItems ci ON ci.CartId = c.Id
                INNER JOIN Products p ON ci.ProductId = p.Id
                WHERE c.UserId = @userId
                ORDER BY ci.Id
            ", new { userId });

            Cart? cart = null;

            items.Read<Cart, CartItem, Product, Cart>((parent, item, product) => {

                if (cart == null) {
                    cart = parent;
                    cart.Items = new List<CartItem>();
                }

                item.Product = product;
                cart.Items?.Add(item);

                return parent;
                
            }, splitOn: "Id,Id");

            return cart;
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

        public async Task<bool> UpdateCartItems(IEnumerable<CartItem> items)
        {
            foreach (var item in items)
                context.Entry<CartItem>(item).State = EntityState.Modified;

            return await context.SaveChangesAsync() > 0;
        }
    }
}