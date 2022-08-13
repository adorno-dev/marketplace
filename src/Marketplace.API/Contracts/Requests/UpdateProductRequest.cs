using System.ComponentModel.DataAnnotations;

namespace Marketplace.API.Contracts.Requests
{
    public class UpdateProductRequest : CreateProductRequest
    {
        [Required]
        public Guid Id { get; set; }
    }
}