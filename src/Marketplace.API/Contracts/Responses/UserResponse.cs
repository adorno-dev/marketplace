namespace Marketplace.API.Contracts.Responses
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
    }
}