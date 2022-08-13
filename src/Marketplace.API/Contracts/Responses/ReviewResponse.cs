namespace Marketplace.API.Contracts.Responses
{
    public class ReviewResponse
    {
        public Guid Id { get; set; }        
        public DateTime Posted { get; set; }
        public string? Text { get; set; }
        public sbyte Rating { get; set; }
        
        public UserResponse? User { get; set; }
        public ProductResponse? Product { get; set; }
    }
}