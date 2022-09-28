namespace Marketplace.API.Contracts.Responses
{
    public class CartItemResponse
    {
        public Guid Id { get; set; }        
        public Guid CartId { get; set; }
        public Guid ProductId { get; set; }

        public string? Description { get; set; }
        public ushort Quantity { get; set; }
        public decimal Price { get; set; }

        public string? Screenshoot { get; set; }
        public CartResponse? Cart { get; set; }
        public ProductResponse? Product { get; set; }
    }
}