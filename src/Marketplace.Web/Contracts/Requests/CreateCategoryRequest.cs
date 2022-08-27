using System.ComponentModel.DataAnnotations;

namespace Marketplace.Web.Contracts.Requests
{
    public class CreateCategoryRequest
    {
        public ushort? ParentId { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(255)]
        public string? Name { get; set; }
    }
}