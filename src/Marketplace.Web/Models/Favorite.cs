namespace Marketplace.Web.Models
{
    public class Favorite
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Store { get; set; }
        public decimal Price { get; set; }
    }
}