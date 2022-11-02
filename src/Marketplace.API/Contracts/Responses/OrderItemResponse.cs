namespace Marketplace.API.Contracts.Responses
{
    public class OrderItemResponse
    {
        public Guid Id { get; set; }
        public Guid? OrderId { get; set; }
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }

        public ushort Quantity { get; set; }
        public decimal? Price { get; set; }
        public string? ProductName { get; set; }
    }
}