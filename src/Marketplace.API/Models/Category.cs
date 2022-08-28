namespace Marketplace.API.Models
{
    public class Category
    {
        public ushort? Id { get; set; } = null;
        public string? Name { get; set; }
        public ushort? ParentId { get; set; }
        public Category? Parent { get; set; }
        public ICollection<Category>? Categories { get; set; }
    }
}