namespace Marketplace.API.Models
{
    public class OrderItem
    {
        public Guid Id { get; set; }
        public Guid? OrderId { get; set; }
        public Guid ProductId { get; set; }
        public Guid StoreId { get; set; }

        public ushort Quantity { get; set; }
        public decimal? Price { get; set; }

        public Order? Order { get; set; }
        public Product? Product { get; set; }
    }
}