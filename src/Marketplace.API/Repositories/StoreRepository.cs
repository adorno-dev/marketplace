using Marketplace.API.Data;
using Marketplace.API.Models;
using Marketplace.API.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.API.Repositories
{
    public class StoreRepository : IStoreRepository
    {
        private readonly AppDbContext context;

        public StoreRepository(AppDbContext context) => this.context = context;

        public async Task<IEnumerable<Store>?> GetStores()
        {
            return await context.Stores.AsNoTracking().ToListAsync();
        }

        public async Task<Store?> GetStore(ushort id)
        {
            return await context.Stores.FindAsync(id);
        }

        public async Task<Store?> CreateStore(Store store)
        {
            context.Stores.Add(store);

            await context.SaveChangesAsync();

            return store;
        }

        public async Task<Store?> UpdateStore(Store store)
        {
            try
            {
                context.Entry<Store>(store).State = EntityState.Modified;       
                
                await context.SaveChangesAsync();

                return store;
            }
            catch { return null; }
        }

        public async Task<Store?> DeleteStore(ushort id)
        {
            var store = await context.Stores.FindAsync(id);

            if (store is not null)
            {
                context.Stores.Remove(store);

                await context.SaveChangesAsync();
            }

            return store;
        }
    }
}