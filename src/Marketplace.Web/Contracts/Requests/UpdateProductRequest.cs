using System.ComponentModel.DataAnnotations;

namespace Marketplace.Web.Contracts.Requests
{
    public class UpdateProductRequest : CreateProductRequest
    {
        [Required]
        public Guid Id { get; set; }
    }
}