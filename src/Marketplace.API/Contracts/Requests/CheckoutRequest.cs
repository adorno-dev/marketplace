using System.ComponentModel.DataAnnotations;

namespace Marketplace.API.Contracts.Requests
{
    public struct CheckoutRequest
    {
        public Guid UserId { get; set; }
        [Required]
        public Guid[] ProductId { get; set; }
        [Required]
        public ushort[] Quantity { get; set; }
    }
}