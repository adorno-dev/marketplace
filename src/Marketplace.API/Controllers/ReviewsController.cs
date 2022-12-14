using Marketplace.API.Contracts.Requests;
using Marketplace.API.Contracts.Responses;
using Marketplace.API.Services.Contracts;
using Marketplace.API.Utils.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.API.Controllers
{
    [Route("api/reviews")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService service;

        public ReviewsController(IReviewService service) => this.service = service;
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReviewResponse>>> GetReviews()
        {
            var categories = await service.GetReviews();

            return Ok(categories);
        }

        [HttpGet("pages/{skip:int?}/{take:int?}")]
        public async Task<ActionResult<IPagination<ReviewResponse>>> GetReviewsPaginated(int skip = 1, int take = 10)
        {
            var reviews = await service.GetReviewsPaginated(skip, take, includeParent: true);

            if (reviews is null)
                return NotFound();

            return Ok(reviews);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReviewResponse?>> GetReview(Guid id)
        {
            var category = await service.GetReview(id);

            if (category is null)
                return NotFound();
            
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CreateReview([FromBody] CreateReviewRequest request)
        {
            if (ModelState.IsValid)                
            {
                return await service.CreateReview(request) ?
                    Ok():
                    BadRequest();
            }

            return BadRequest(ModelState.Values);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateReview([FromBody] UpdateReviewRequest request)
        {
            if (ModelState.IsValid)                
            {
                return await service.UpdateReview(request) ?
                    Ok():
                    BadRequest();
            }

            return BadRequest(ModelState.Values);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(Guid id)
        {
            return await service.DeleteReview(id) ?
                Ok():
                NotFound();
        }
    }
}