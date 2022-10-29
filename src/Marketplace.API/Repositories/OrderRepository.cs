using Marketplace.API.Data;
using Marketplace.API.Models;
using Marketplace.API.Repositories.Contracts;

namespace Marketplace.API.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DatabaseContext context;

        public OrderRepository(DatabaseContext context) => this.context = context;

        public async Task<bool> PlaceOrder(Order order)
        {
            int rowsAffected;
            
            await context.Database.BeginTransactionAsync();
            context.Add(order);
            await context.Database.CommitTransactionAsync();
            rowsAffected =  await context.SaveChangesAsync();
            
            return rowsAffected > 0;
        }
    }
}