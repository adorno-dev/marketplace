namespace Marketplace.API.Models
{
    public class Store
    {
        public ushort Id { get; set; }
        public string? Name { get; set; }
        public string? Categories { get; set; }
        public DateTime Joined { get; set; }
    }
}