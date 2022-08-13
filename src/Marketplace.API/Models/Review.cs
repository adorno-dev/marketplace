namespace Marketplace.API.Models
{
    public class Review
    {
        public Guid Id { get; set; }        
        public DateTime Posted { get; set; }
        public string? Text { get; set; }
        public sbyte Rating { get; set; }
        
        public Guid UserId { get; set; }
        public User? User { get; set; }

        public Guid ProductId { get; set; }
        public Product? Product { get; set; }
    }
}