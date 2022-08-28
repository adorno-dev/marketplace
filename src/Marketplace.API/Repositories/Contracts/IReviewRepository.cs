using Marketplace.API.Models;
using Marketplace.API.Utils.Contracts;

namespace Marketplace.API.Repositories.Contracts
{
    public interface IReviewRepository
    {
        Task<IEnumerable<Review>?> GetReviews();
        Task<IPagination<Review>?> GetReviewsPaginated(int skip, int take, bool includeParent = false);
        Task<Review?> GetReview(Guid id);
        Task<bool> CreateReview(Review review);
        Task<bool> UpdateReview(Review review);
        Task<bool> DeleteReview(Guid id);  
    }
}