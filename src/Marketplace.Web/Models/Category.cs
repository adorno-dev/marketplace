namespace Marketplace.Web.Models
{
    public class Category
    {
        public ushort Id { get; set; }
        public ushort? ParentId { get; set; }
        public string? Name { get; set; }
    }
}