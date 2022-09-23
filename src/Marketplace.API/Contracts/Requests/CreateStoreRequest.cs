using System.ComponentModel.DataAnnotations;

namespace Marketplace.API.Contracts.Requests
{
    [Serializable]
    public class CreateStoreRequest
    {
        [Required]
        [MinLength(3)]
        [MaxLength(255)]
        public string? Name { get; set; }
        public string? Url { get; set; }
        public string? Profile { get; set; }
        public string? Politics { get; set; }

        public IFormFile? Logo { get; set; }
        public IFormFile? Banner { get; set; }

        [Required]
        public Guid UserId { get; set; }
    }
}