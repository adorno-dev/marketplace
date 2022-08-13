using Marketplace.API.Data;
using Marketplace.API.Models;
using Marketplace.API.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.API.Repositories
{
    public class StoreRepository : IStoreRepository
    {
        private readonly DatabaseContext context;

        public StoreRepository(DatabaseContext context) => this.context = context;

        public async Task<IEnumerable<Store>?> GetStores()
        {
            return await context.Stores.AsNoTracking().ToListAsync();
        }

        public async Task<Store?> GetStore(Guid id)
        {
            return await context.Stores.FindAsync(id);
        }

        public async Task<bool> CreateStore(Store store)
        {
            context.Stores.Add(store);

            return await context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateStore(Store store)
        {
            context.Entry<Store>(store).State = EntityState.Modified;
            
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteStore(Guid id)
        {
            var store = await context.Stores.FindAsync(id);

            if (store is not null)
            {
                context.Stores.Remove(store);

                return await context.SaveChangesAsync() > 0;
            }

            return false;
        }
    }
}