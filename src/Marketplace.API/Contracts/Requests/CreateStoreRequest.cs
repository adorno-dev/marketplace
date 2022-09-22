using System.ComponentModel.DataAnnotations;

namespace Marketplace.API.Contracts.Requests
{
    public class CreateStoreRequest
    {
        [Required]
        [MinLength(3)]
        [MaxLength(255)]
        public string? Name { get; set; }
        public string? Url { get; set; }
        public string? Profile { get; set; }
        public string? Politics { get; set; }

        [Required]
        public Guid UserId { get; set; }
    }
}