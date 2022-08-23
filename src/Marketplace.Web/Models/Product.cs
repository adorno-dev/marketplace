namespace Marketplace.Web.Models
{
    public class Product
    {
        public Guid Id { get; set; }        
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public long Stock { get; set; }
        
        public Guid StoreId { get; set; }
        public ushort CategoryId { get; set; }

        public Store? Store { get; set; }
        public Category? Category { get; set; }
    }
}