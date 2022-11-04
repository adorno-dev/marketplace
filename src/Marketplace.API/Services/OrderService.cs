using AutoMapper;
using Marketplace.API.Contracts.Responses;
using Marketplace.API.Models;
using Marketplace.API.Repositories.Contracts;
using Marketplace.API.Services.Contracts;
using Marketplace.API.UnitOfWorks.Contracts;

namespace Marketplace.API.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderUnitOfWork orderUnitOfWork;

        private IMapper mapper;

        public OrderService(IOrderUnitOfWork orderUnitOfWork, IMapper mapper)
        {
            this.orderUnitOfWork = orderUnitOfWork;
            this.mapper = mapper;
        }

        public async Task<IList<OrderItemResponse?>> GetOrders(Guid storeId)
        {
            var orderItems = await orderUnitOfWork.GetOrders(storeId);

            return mapper.Map<IList<OrderItemResponse?>>(orderItems);
        }

        public Task<Guid?> PlaceOrder(Guid userId)
        {
            return orderUnitOfWork.PlaceOrder(userId);
        }
    }
}