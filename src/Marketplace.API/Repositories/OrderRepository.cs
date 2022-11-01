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
            int rowsAffected = 0;

            try
            {
                await context.Database.BeginTransactionAsync();
                context.Add(order);
                rowsAffected =  await context.SaveChangesAsync();                
                await context.Database.CommitTransactionAsync();
            }
            catch
            {
                await context.Database.RollbackTransactionAsync();
            }
            
            
            return rowsAffected > 0;
        }
    }
}