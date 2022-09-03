using System.ComponentModel.DataAnnotations;

namespace Marketplace.Web.Contracts.Requests
{
    public class AddCartItemRequest
    {
        [Required]
        public Guid UserId { get; set; }
        
        [Required]
        public Guid ProductId { get; set; }        
    }
}