using System.ComponentModel.DataAnnotations;

namespace Marketplace.Web.Contracts.Requests
{
    public class ForgotPasswordRequest
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }        
    }
}