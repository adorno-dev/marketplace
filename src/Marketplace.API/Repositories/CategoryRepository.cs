using Marketplace.API.Data;
using Marketplace.API.Models;
using Marketplace.API.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.API.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext context;

        public CategoryRepository(AppDbContext context) => this.context = context;

        public async Task<IEnumerable<Category>?> GetCategories(bool includeParent = false)
        {
            return includeParent ?
                await context.Categories.Include(c => c.Parent).Include("Parent.Parent").AsNoTracking().ToListAsync():
                await context.Categories.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Category>?> GetCategories(params ushort[] ids)
        {
            return await context.Categories.Where(c => ids.Any(w => w.Equals(c.Id))).ToListAsync();
        }

        public async Task<Category?> GetCategory(ushort id, bool includeParent = false)
        {
            return includeParent ?
                await context.Categories.Include(c => c.Parent).FirstOrDefaultAsync(c => c.Id.Equals(id)):
                await context.Categories.FindAsync(id);
        }

        public async Task<Category?> CreateCategory(Category category)
        {
            context.Categories.Add(category);
            
            await context.SaveChangesAsync();

            return category;
        }

        public async Task<Category?> UpdateCategory(Category category)
        {
            try
            {
                context.Entry<Category>(category).State = EntityState.Modified;

                await context.SaveChangesAsync();

                return category;
            }
            catch { return null; }
        }

        public async Task<Category?> DeleteCategory(ushort id)
        {
            var category = await context.Categories.Include(c => c.Categories).FirstOrDefaultAsync(c => c.Id.Equals(id));

            if (category is not null)
            {
                if (category.Categories?.Count > 0)
                    context.RemoveRange(category.Categories);

                context.Categories.Remove(category);

                await context.SaveChangesAsync();
            }

            return category;
        }
    }
}