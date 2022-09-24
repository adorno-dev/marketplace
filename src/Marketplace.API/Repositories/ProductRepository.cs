using Marketplace.API.Data;
using Marketplace.API.Models;
using Marketplace.API.Repositories.Contracts;
using Marketplace.API.Utils;
using Marketplace.API.Utils.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DatabaseContext context;

        public ProductRepository(DatabaseContext context) => this.context = context;

        public async Task<IEnumerable<Product>?> GetProducts(Guid userId)
        {
            return await context.Products
                .AsNoTracking()
                // .Include(c => c.Category)
                // .Include(s => s.Store)
                .ToListAsync();
        }

        public async Task<IPagination<Product>?> GetProductsPaginated(Guid userId, int skip, int take, bool includeParent = false)
        {
            var products = new Pagination<Product>();

            products.PageIndex = skip <= 0 ? 1 : skip;
            products.PageSize = take;
            products.SetCount(await context.Products.AsNoTracking().Select(p => p.Id).CountAsync());

            products.Items = await context.Products
                .Include(c => c.Category)
                .Include(s => s.Store)
                .AsNoTracking()
                .Select(s => new Product {
                    Id = s.Id,
                    Name = s.Name,
                    Description = s.Description,
                    Price = s.Price,
                    Stock = s.Stock,
                    Favorite = context.Favorites.Any(w => w.ProductId.Equals(s.Id) && w.UserId.Equals(userId)),
                    Category = s.Category,
                    Store = s.Store != null ? new Store 
                    { 
                        Id = s.Store.Id, 
                        Name = s.Store.Name,
                    } : null,
                    
                })
                .Skip((products.PageIndex - 1) * products.PageSize)
                .Take(products.PageSize)
                .ToListAsync();

            return products.Items.Any() ? products : null;
        }

        public async Task<Product?> GetProduct(Guid userId, Guid id)
        {
            return await context.Products
                .Include(c => c.Category)
                .Include(s => s.Store)
                .AsNoTracking()
                .Select(s => new Product {
                    Id = s.Id,
                    Name = s.Name,
                    Description = s.Description,
                    Price = s.Price,
                    Stock = s.Stock,
                    Favorite = context.Favorites.Any(w => w.ProductId.Equals(s.Id) && w.UserId.Equals(userId)),
                    Category = s.Category,
                    Store = s.Store != null ? new Store { Id = s.Store.Id, Name = s.Store.Name } : null
                })
                .FirstOrDefaultAsync(p => p.Id.Equals(id));
        }

        public async Task<bool> CreateProduct(Product product)
        {
            context.Products.Add(product);

            return await context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            context.Entry<Product>(product).State = EntityState.Modified;       
            
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteProduct(Guid id)
        {
            var product = await context.Products.FindAsync(id);

            if (product is not null)
            {
                context.Products.Remove(product);

                return await context.SaveChangesAsync() > 0;
            }

            return false;
        }
    }
}