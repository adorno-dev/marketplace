using System.ComponentModel.DataAnnotations;

namespace Marketplace.API.Contracts.Requests
{
    public class SignInRequest
    {
        [Required]
        public string? Email { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}