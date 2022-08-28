using Marketplace.API.Contracts.Requests;
using Marketplace.API.Contracts.Responses;
using Marketplace.API.Utils.Contracts;

namespace Marketplace.API.Services.Contracts
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewResponse>?> GetReviews();
        Task<IPagination<ReviewResponse>?> GetReviewsPaginated(int skip, int take, bool includeParent = false);
        Task<ReviewResponse?> GetReview(Guid id);
        Task<bool> CreateReview(CreateReviewRequest request);
        Task<bool> UpdateReview(UpdateReviewRequest request);
        Task<bool> DeleteReview(Guid id);
    }
}