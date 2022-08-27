namespace Marketplace.Web.Models
{
    public class Store
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public IEnumerable<ushort>? Categories { get; set; }
        public Guid UserId { get; set; }        
    }
}