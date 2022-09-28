using System.ComponentModel.DataAnnotations;

namespace Marketplace.API.Contracts.Requests
{
    public class AddCartItemRequest
    {
        public Guid UserId { get; set; }
        
        [Required]
        public Guid ProductId { get; set; }
    }
}