namespace Marketplace.API.Models
{
    public class CartItem
    {
        public Guid Id { get; set; }        
        public Guid? CartId { get; set; }
        public Guid ProductId { get; set; }
        public Guid StoreId { get; set; }

        public string? Description { get; set; }
        public ushort Quantity { get; set; }
        public decimal? Price { get; set; }

        public Cart? Cart { get; set; }
        public Product? Product { get; set; }
    }
}