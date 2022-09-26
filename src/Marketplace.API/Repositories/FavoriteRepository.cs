using Marketplace.API.Data;
using Marketplace.API.Models;
using Marketplace.API.Repositories.Contracts;
using Marketplace.API.Utils;
using Marketplace.API.Utils.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.API.Repositories
{
    public class FavoriteRepository : IFavoriteRepository
    {
        private readonly DatabaseContext context;

        public FavoriteRepository(DatabaseContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Favorite>?> GetFavorites(Guid userId)
        {
            return await context.Favorites.AsNoTracking().OrderBy(o => o.Product).Where(f => f.UserId.Equals(userId)).ToListAsync();
        }

        public async Task<IPagination<Favorite>?> GetFavoritesPaginated(Guid userId, int skip, int take)
        {
            var favorites = new Pagination<Favorite>();

            favorites.PageIndex = skip <= 0 ? 1 : skip;
            favorites.PageSize = take;
            favorites.SetCount(await context.Favorites.Where(f => f.UserId.Equals(userId)).AsNoTracking().CountAsync());

            favorites.Items = await context.Favorites
                .Include("Product")
                .Include("Product.Store")
                .AsNoTracking()
                .OrderBy(o => o.Product)
                .Where(f => f.UserId.Equals(userId))
                .Skip((favorites.PageIndex - 1) * favorites.PageSize)
                .Take(favorites.PageSize)
                .ToListAsync();

            return favorites;
        }

        public async Task<Favorite?> GetFavorite(Guid userId, Guid productId)
        {
            return await context.Favorites
                .AsNoTracking()
                .OrderBy(o => o.Product)
                .Where(f => f.UserId.Equals(userId) && f.ProductId.Equals(productId))
                .FirstOrDefaultAsync();
        }

        public async Task<bool> Favorite(Favorite favorite)
        {
            try
            {
                context.Favorites.Add(favorite);

                return await context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }

        public async Task<bool> Unfavorite(Favorite favorite)
        {
            context.Favorites.Remove(favorite);

            return await context.SaveChangesAsync() > 0;
        }
    }
}