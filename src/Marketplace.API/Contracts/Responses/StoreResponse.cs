namespace Marketplace.API.Contracts.Responses
{
    public class StoreResponse
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public IEnumerable<ushort>? Categories { get; set; }
        public UserResponse? User { get; set; }
    }
}