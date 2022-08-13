using AutoMapper;
using Marketplace.API.Contracts.Requests;
using Marketplace.API.Contracts.Responses;
using Marketplace.API.Models;
using Marketplace.API.Repositories.Contracts;
using Marketplace.API.Services.Contracts;

namespace Marketplace.API.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IMapper mapper;
        private readonly IReviewRepository repository;

        public ReviewService(IMapper mapper, IReviewRepository repository)
        {
            this.mapper = mapper;
            this.repository = repository;
        }

        public async Task<IEnumerable<ReviewResponse>?> GetReviews()
        {
            var reviews = await repository.GetReviews();

            return mapper.Map<IEnumerable<ReviewResponse>?>(reviews);
        }

        public async Task<ReviewResponse?> GetReview(Guid id)
        {
            var product = await repository.GetReview(id);

            return mapper.Map<ReviewResponse>(product);
        }

        public async Task<bool> CreateReview(CreateReviewRequest request)
        {
            var product = mapper.Map<Review>(request);

            return await repository.CreateReview(product);
        }

        public async Task<bool> UpdateReview(UpdateReviewRequest request)
        {
            var product = mapper.Map<Review>(request);

            return await repository.UpdateReview(product);
        }

        public async Task<bool> DeleteReview(Guid id)
        {
            return await repository.DeleteReview(id);
        }
    }
}