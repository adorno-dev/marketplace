namespace Marketplace.Web.Models
{
    public class Category
    {
        public ushort Id { get; set; }
        public string? Name { get; set; }

        public Category? Parent { get; set; }
    }
}