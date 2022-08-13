using Marketplace.API.Data;
using Marketplace.API.Models;
using Marketplace.API.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DatabaseContext context;

        public ProductRepository(DatabaseContext context) => this.context = context;

        public async Task<IEnumerable<Product>?> GetProducts()
        {
            return await context.Products.Include(c => c.Category).Include(s => s.Store).ToListAsync();
        }

        public async Task<Product?> GetProduct(Guid id)
        {
            return await context.Products.Include(c => c.Category).Include(s => s.Store).FirstOrDefaultAsync(p => p.Id.Equals(id));
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