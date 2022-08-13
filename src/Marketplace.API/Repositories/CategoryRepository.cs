using Marketplace.API.Data;
using Marketplace.API.Models;
using Marketplace.API.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.API.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DatabaseContext context;

        public CategoryRepository(DatabaseContext context) => this.context = context;

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

        public async Task<bool> CreateCategory(Category category)
        {
            context.Categories.Add(category);
            
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateCategory(Category category)
        {
            context.Entry<Category>(category).State = EntityState.Modified;

            return await context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteCategory(ushort id)
        {
            var category = await context.Categories.Include(c => c.Categories).FirstOrDefaultAsync(c => c.Id.Equals(id));

            if (category is not null)
            {
                if (category.Categories?.Count > 0)
                    context.RemoveRange(category.Categories);

                context.Categories.Remove(category);

                return await context.SaveChangesAsync() > 0;
            }

            return false;
        }
    }
}