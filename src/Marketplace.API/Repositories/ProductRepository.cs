using Marketplace.API.Data;
using Marketplace.API.Models;
using Marketplace.API.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext context;

        public ProductRepository(AppDbContext context) => this.context = context;

        public async Task<IEnumerable<Product>?> GetProducts()
        {
            return await context.Products.Include(c => c.Category).Include(s => s.Store).ToListAsync();
        }

        public async Task<Product?> GetProduct(Guid id)
        {
            return await context.Products.Include(c => c.Category).Include(s => s.Store).FirstOrDefaultAsync(p => p.Id.Equals(id));
        }

        public async Task<Product?> CreateProduct(Product product)
        {
            context.Products.Add(product);

            await context.SaveChangesAsync();

            return product;
        }

        public async Task<Product?> UpdateProduct(Product product)
        {
            try
            {
                context.Entry<Product>(product).State = EntityState.Modified;       
                
                await context.SaveChangesAsync();

                return product;
            }
            catch { return null; }
        }

        public async Task<Product?> DeleteProduct(Guid id)
        {
            var product = await context.Products.FindAsync(id);

            if (product is not null)
            {
                context.Products.Remove(product);

                await context.SaveChangesAsync();
            }

            return product;
        }
    }
}