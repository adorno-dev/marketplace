namespace Marketplace.API.Models
{
    public class Store
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Categories { get; set; }

        public Guid UserId { get; set; }
        public User? User { get; set; }
    }
}