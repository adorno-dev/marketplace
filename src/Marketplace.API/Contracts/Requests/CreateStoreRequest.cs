using System.ComponentModel.DataAnnotations;

namespace Marketplace.API.Contracts.Requests
{
    public class CreateStoreRequest
    {
        [Required]
        [MinLength(3)]
        [MaxLength(255)]
        public string? Name { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public IEnumerable<ushort>? Categories { get; set; }
    }
}