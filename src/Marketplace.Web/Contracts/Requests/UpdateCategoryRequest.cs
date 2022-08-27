using System.ComponentModel.DataAnnotations;

namespace Marketplace.Web.Contracts.Requests
{
    public class UpdateCategoryRequest : CreateCategoryRequest
    {
        [Required]
        public ushort Id { get; set; }   
    }
}