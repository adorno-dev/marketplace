using System.ComponentModel.DataAnnotations;

namespace Marketplace.API.Contracts.Requests
{
    public class CreateReviewRequest
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid ProductId { get; set; }

        [Required]
        public string? Text { get; set; }

        [Required]
        [Range(1, 5)]
        public sbyte Rating { get; set; }
    }
}