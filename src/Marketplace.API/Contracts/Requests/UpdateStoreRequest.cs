using System.ComponentModel.DataAnnotations;

namespace Marketplace.API.Contracts.Requests
{
    public class UpdateStoreRequest
    {
        public ushort Id { get; set; }
        
        [Required]
        [MinLength(3)]
        [MaxLength(255)]
        public string? Name { get; set; }

        [Required]
        public IEnumerable<ushort>? Categories { get; set; }
    }
}