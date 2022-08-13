using System.ComponentModel.DataAnnotations;

namespace Marketplace.API.Contracts.Requests
{
    public class UpdateCategoryRequest : CreateCategoryRequest
    {
        [Required]
        public ushort Id { get; set; }
    }
}