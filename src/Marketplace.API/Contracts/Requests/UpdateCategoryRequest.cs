using System.ComponentModel.DataAnnotations;

namespace Marketplace.API.Contracts.Requests
{
    public class UpdateCategoryRequest
    {
        public ushort Id { get; set; }

        public ushort? ParentId { get; set; }
        
        [Required]
        [MinLength(3)]
        [MaxLength(255)]
        public string? Name { get; set; }
    }
}