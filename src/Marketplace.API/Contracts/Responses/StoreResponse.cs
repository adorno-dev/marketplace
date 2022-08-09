namespace Marketplace.API.Contracts.Responses
{
    public class StoreResponse
    {
        public ushort Id { get; set; }
        public string? Name { get; set; }
        public IEnumerable<ushort>? Categories { get; set; }
        public DateTime Joined { get; set; }
    }
}