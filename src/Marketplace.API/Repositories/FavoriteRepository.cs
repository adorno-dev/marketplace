using Dapper;
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
            // favorites.SetCount(await context.Favorites.Where(f => f.UserId.Equals(userId)).AsNoTracking().CountAsync());

            // favorites.Items = await context.Favorites
            //     .Include("Product")
            //     // .Include("Product.Store")
            //     .AsNoTracking()
            //     .OrderBy(o => o.Product)
            //     .Where(f => f.UserId.Equals(userId))
            //     .Skip((favorites.PageIndex - 1) * favorites.PageSize)
            //     .Take(favorites.PageSize)
            //     .ToListAsync();

            // favorites.Items = await context.Database.GetDbConnection().QueryAsync<Favorite, Product, Favorite>(
            //     // TODO: Create SQL VIEW on Database Seed.
            //     @$"
            //         SELECT
            //             f.UserId,
            //             f.ProductId,
            //             p.Id,
            //             p.StoreId,
            //             p.Name,
            //             p.Price
            //         FROM Favorites f
            //         INNER JOIN Products p ON  p.Id = f.ProductId
            //         WHERE f.UserId = '{userId}'
            //         ORDER BY f.UserId
            //         OFFSET {(favorites.PageIndex - 1) * favorites.PageSize} ROWS
            //         FETCH NEXT {favorites.PageSize} ROWS ONLY

            //     ",
            //     (favorite, product) => 
            //     {
            //         favorite.Product = product;
            //         return favorite;
            //     }
            // );

            
            var items = await context.Database.GetDbConnection().QueryMultipleAsync(@$"
                SELECT COUNT(UserId) TotalItems FROM Favorites WHERE UserId = @userId
                SELECT
                    f.UserId,
                    f.ProductId,
                    p.Id,
                    p.StoreId,
                    p.Name,
                    p.Price
                FROM Favorites f
                INNER JOIN Products p ON  p.Id = f.ProductId
                WHERE f.UserId = @userId
                ORDER BY f.UserId
                OFFSET (@pageIndex - 1) * @pageSize ROWS
                FETCH NEXT @pageSize ROWS ONLY
            ", new { userId, pageIndex = favorites.PageIndex, pageSize = favorites.PageSize });

            favorites.SetCount(items.Read<int>().Single());
            favorites.Items = items.Read<Favorite, Product, Favorite>((favorite, product) => {
                favorite.Product = product;
                return favorite;
            });

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