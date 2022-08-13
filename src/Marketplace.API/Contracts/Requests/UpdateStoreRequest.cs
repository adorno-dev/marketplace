using System.ComponentModel.DataAnnotations;

namespace Marketplace.API.Contracts.Requests
{
    public class UpdateStoreRequest : CreateStoreRequest
    {
        [Required]
        public Guid Id { get; set; }
    }
}