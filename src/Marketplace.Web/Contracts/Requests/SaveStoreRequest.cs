using System.ComponentModel.DataAnnotations;

namespace Marketplace.Web.Contracts.Requests
{
    public class SaveStoreRequest
    {
        [Required]
        public Guid? Id { get; set; }
        
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