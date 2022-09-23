namespace Marketplace.API.Contracts.Responses
{
    public sealed class StoreResponse
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Url { get; set; }
        public string? Profile { get; set; }
        public string? Politics { get; set; }
        public UserResponse? User { get; set; }
    }
}