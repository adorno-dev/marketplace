using System.ComponentModel.DataAnnotations;

namespace Marketplace.API.Contracts.Requests
{
    public class DeleteCartItemRequest
    {
        public Guid UserId { get; set; }
        
        [Required]
        public Guid CartItemId { get; set; }
    }
}