namespace Marketplace.API.Contracts.Responses
{
    public class ProductResponse
    {
        public Guid Id { get; set; }

        public string? Screenshoot { get; set; }
        public string[]? Screenshoots { get; set; }

        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public long Stock { get; set; }
        public bool Favorite { get; set; }

        public ushort? categoryId { get; set; }
        public Guid? StoreId { get; set; }

        public StoreResponse? Store { get; set; }
        public CategoryResponse? Category { get; set; }
    }
}