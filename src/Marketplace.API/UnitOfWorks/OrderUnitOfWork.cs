using Marketplace.API.Data;
using Marketplace.API.Models;
using Marketplace.API.Repositories;
using Marketplace.API.Repositories.Contracts;
using Marketplace.API.UnitOfWorks.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.API.UnitOfWorks
{
    public class OrderUnitOfWork : IOrderUnitOfWork
    {
        private readonly DatabaseContext context;
        private readonly ICartRepository cartRepository;
        private readonly IOrderRepository orderRepository;

        public OrderUnitOfWork(DatabaseContext context)
        {
            this.context = context;

            cartRepository = new CartRepository(this.context);
            orderRepository = new OrderRepository(this.context);
        }

        public async Task Commit()
        {
            await context.Database.CommitTransactionAsync();
        }

        public async Task Rollback()
        {
            await context.Database.RollbackTransactionAsync();
        }

        public async Task<bool> PlaceOrder(Guid userId)
        {
            var cart = await cartRepository.GetCart(userId);

            if (cart is not null && cart.Items?.Count > 0)
            {
                var order = new Order(userId);

                var success = false;

                order.Items = cart.Items.Select(s => new OrderItem
                {
                    OrderId = order.Id,
                    ProductId = s.ProductId,
                    StoreId = s.StoreId,
                    Quantity = s.Quantity,
                    Price = s.Price

                }).ToList();

                await context.Database.BeginTransactionAsync();
                context.Orders.Add(order);
                context.Carts.Remove(cart);

                success = await context.SaveChangesAsync() > 0;

                if (success)
                    await Commit();
                else
                    await Rollback();

                return success;
            }

            return false;
        }
    }
}