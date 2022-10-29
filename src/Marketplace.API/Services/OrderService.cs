using Marketplace.API.Models;
using Marketplace.API.Repositories.Contracts;
using Marketplace.API.Services.Contracts;

namespace Marketplace.API.Services
{
    public class OrderService : IOrderService
    {
        private readonly ICartRepository cartRepository;
        private readonly IOrderRepository orderRepository;

        public OrderService(ICartRepository cartRepository, IOrderRepository orderRepository)
        {
            this.cartRepository = cartRepository;
            this.orderRepository = orderRepository;
        }

        public async Task<bool> PlaceOrder(Guid userId)
        {
            var cart = await cartRepository.GetCart(userId);

            if (cart != null && cart.Items?.Count > 0)
            {
                var order = new Order(userId);

                order.CartId = cart.Id;
                order.Items = cart.Items.Select(o => new OrderItem
                {
                    OrderId = order.Id,
                    ProductId = o.ProductId,
                    StoreId = o.StoreId,
                    Quantity = o.Quantity,
                    Price = o.Price
                }).ToList();

                return await orderRepository.PlaceOrder(order);
            }

            return false;
        }
    }
}