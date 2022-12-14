using System.ComponentModel.DataAnnotations;

namespace Marketplace.Web.Contracts.Requests
{
    public class DeleteCartItemRequest
    {
        [Required]
        public Guid UserId { get; set; }
        
        [Required]
        public Guid CartItemId { get; set; }        
    }
}