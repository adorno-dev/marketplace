using System.ComponentModel.DataAnnotations;

namespace Marketplace.Web.Contracts.Requests
{
    public class PlaceOrderRequest
    {
        [Required]
        public string? FirstName { get; set; }
        
        [Required]
        public string? LastName { get; set; }
        
        [Required]
        public string? Email { get; set; }
        
        [Required]
        public string? PhoneNumber { get; set; }
        

        [Required]
        public string? CardNumber { get; set; }
        
        [Required]
        public string? NameOnCard { get; set; }
        
        [Required]
        public string? CVV { get; set; }
        
        [Required]
        public string? ExpireDate { get; set; }
        
    }
}