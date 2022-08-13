using Marketplace.API.Data;
using Marketplace.API.Models;
using Marketplace.API.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.API.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly DatabaseContext context;

        public ReviewRepository(DatabaseContext context) => this.context = context;

        public async Task<IEnumerable<Review>?> GetReviews()
        {
            return await context.Reviews.ToListAsync();
        }

        public async Task<Review?> GetReview(Guid id)
        {
            return await context.Reviews.FirstOrDefaultAsync(p => p.Id.Equals(id));
        }

        public async Task<bool> CreateReview(Review review)
        {
            context.Reviews.Add(review);

            return await context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateReview(Review review)
        {
            context.Entry<Review>(review).State = EntityState.Modified;       
            
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteReview(Guid id)
        {
            var review = await context.Reviews.FindAsync(id);

            if (review is not null)
            {
                context.Reviews.Remove(review);

                return await context.SaveChangesAsync() > 0;
            }

            return false;
        }
    }
}