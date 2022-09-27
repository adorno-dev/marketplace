namespace Marketplace.API.Contracts.Responses
{
    public class FavoriteResponse
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Store { get; set; }
        public string? Screenshoot { get; set; }
        public decimal Price { get; set; }
    }
}