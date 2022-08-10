using Marketplace.API.Models;

namespace Marketplace.API.Contracts.Responses
{
    public class ProductResponse
    {
        public Guid Id { get; set; }        
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public long Stock { get; set; }

        public Store? Store { get; set; }
        public Category? Category { get; set; }
    }
}