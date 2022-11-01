using Marketplace.API.Models;
using Marketplace.API.Repositories.Contracts;
using Marketplace.API.Services.Contracts;
using Marketplace.API.UnitOfWorks.Contracts;

namespace Marketplace.API.Services
{
    public class OrderService : IOrderService
    {
        // private readonly ICartRepository cartRepository;
        // private readonly IOrderRepository orderRepository;

        // public OrderService(ICartRepository cartRepository, IOrderRepository orderRepository)
        // {
        //     this.cartRepository = cartRepository;
        //     this.orderRepository = orderRepository;
        // }

        // // TODO: Unit of work approach.

        // public async Task<bool> PlaceOrder(Guid userId)
        // {
        //     var cart = await cartRepository.GetCart(userId);

        //     if (cart != null && cart.Items?.Count > 0)
        //     {
        //         var order = new Order(userId);

        //         order.CartId = cart.Id;
        //         order.Items = cart.Items.Select(o => new OrderItem
        //         {
        //             OrderId = order.Id,
        //             ProductId = o.ProductId,
        //             StoreId = o.StoreId,
        //             Quantity = o.Quantity,
        //             Price = o.Price
        //         }).ToList();
 
        //         var placeOrder = await orderRepository.PlaceOrder(order);
        //         var removeCart = await cartRepository.DeleteCart(cart); 


        //         if (await orderRepository.PlaceOrder(order))
        //             return await cartRepository.DeleteCart(cart);
        //     }

        //     return false;
        // }

        private readonly IOrderUnitOfWork orderUnitOfWork;

        public OrderService(IOrderUnitOfWork orderUnitOfWork)
        {
            this.orderUnitOfWork = orderUnitOfWork;
        }

        public Task<bool> PlaceOrder(Guid userId)
        {
            return orderUnitOfWork.PlaceOrder(userId);
        }
    }
}