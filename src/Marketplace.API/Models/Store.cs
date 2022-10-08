namespace Marketplace.API.Models
{
    public class Store
    {
        public Guid Id { get; set; }
        public DateTime Joined { get; set; }

        public string? Name { get; set; }
        public string? Url { get; set; }
        public string? Profile { get; set; }
        public string? Politics { get; set; }

        public Guid? UserId { get; set; }
        public User? User { get; set; }

        public ICollection<Product>? Products { get; set; }
    }
}