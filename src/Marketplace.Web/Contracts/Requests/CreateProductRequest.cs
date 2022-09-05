using System.ComponentModel.DataAnnotations;

namespace Marketplace.Web.Contracts.Requests
{
    public class CreateProductRequest
    {
        public Guid Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(255)]
        public string? Name { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(2500)]
        public string? Description { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal? Price { get; set; }

        [Required]
        [Range(1, 9999)]
        public long? Stock { get; set; }

        [Required]
        public Guid? StoreId { get; set; }

        [Required]
        public ushort? CategoryId { get; set; }

        public IFormFileCollection? Images { get; set; }

        public CreateProductRequest()
        {
            Id = Guid.NewGuid();
        }
    }
}