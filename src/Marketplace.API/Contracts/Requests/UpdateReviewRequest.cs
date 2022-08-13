using System.ComponentModel.DataAnnotations;

namespace Marketplace.API.Contracts.Requests
{
    public class UpdateReviewRequest
    {
        [Required]
        public Guid Id { get; set; } 

        [Required]
        public string? Text { get; set; }

        [Required]
        public sbyte Rating { get; set; }
    }
}