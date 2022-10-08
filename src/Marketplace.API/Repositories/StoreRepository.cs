using Marketplace.API.Data;
using Marketplace.API.Models;
using Marketplace.API.Repositories.Contracts;
using Marketplace.API.Utils;
using Marketplace.API.Utils.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.API.Repositories
{
    public sealed class StoreRepository : IStoreRepository
    {
        private readonly DatabaseContext context;

        public StoreRepository(DatabaseContext context) => this.context = context;

        public async Task<IEnumerable<Store>?> GetStores()
        {
            return await context.Stores.Include("User").AsNoTracking().ToListAsync();
        }

        public async Task<IPagination<Store>?> GetStoresPaginated(int skip, int take)
        {
            var stores = new Pagination<Store>();

            stores.PageIndex = skip <= 0 ? 1 : skip;
            stores.PageSize = take;
            stores.SetCount(await context.Stores.Select(s => s.Id).CountAsync());
            stores.Items = await context.Stores
                .Include("User")
                .AsNoTracking()
                .Select(s => new Store
                {
                    Id = s.Id,
                    Name = s.Name,
                    Url = s.Url,
                    Profile = s.Profile,
                    Politics = s.Politics,
                    UserId = s.UserId,
                    User = s.User != null ? new () { 
                         
                        UserName = s.User.UserName, 
                        Email = s.User.Email 
                    } : null
                })
                .OrderBy(o => o.Name)
                .Skip((stores.PageIndex - 1) * stores.PageSize)
                .Take(stores.PageSize)
                .ToListAsync();

            return stores;
        }

        public async Task<Store?> GetStore(Guid id)
        {
            return await context.Stores.Include("User").Include(x => x.Products).AsNoTracking().FirstOrDefaultAsync(s => s.Id.Equals(id));
        }

        public async Task<Store?> GetStoreByUserId(Guid userId)
        {
            return await context.Stores.AsNoTracking().Include("User").FirstOrDefaultAsync(s => s.UserId.Equals(userId));
        }

        public async Task<Guid?> GetStoreIdByUserId(Guid userId)
        {
            return await context.Stores.AsNoTracking().Where(s => s.UserId.Equals(userId)).Select(s => s.Id).FirstOrDefaultAsync();
        }

        public async Task<Guid?> CreateStore(Store store)
        {
            context.Stores.Add(store);

            return await context.SaveChangesAsync() > 0 ?
                store.Id : null;
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