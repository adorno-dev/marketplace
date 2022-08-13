using Marketplace.API.Models;

namespace Marketplace.API.Repositories.Contracts
{
    public interface IReviewRepository
    {
        Task<IEnumerable<Review>?> GetReviews();
        Task<Review?> GetReview(Guid id);
        Task<bool> CreateReview(Review review);
        Task<bool> UpdateReview(Review review);
        Task<bool> DeleteReview(Guid id);  
    }
}