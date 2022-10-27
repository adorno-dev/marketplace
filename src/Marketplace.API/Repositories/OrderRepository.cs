using Marketplace.API.Data;
using Marketplace.API.Repositories.Contracts;

namespace Marketplace.API.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DatabaseContext context;

        public OrderRepository(DatabaseContext context) => this.context = context;
    }
}