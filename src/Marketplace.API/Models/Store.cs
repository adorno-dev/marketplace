using Marketplace.API.Utils;

namespace Marketplace.API.Models
{
    public class Store : Pagination<Product>
    {
        public Guid Id { get; set; }
        public DateTime Joined { get; set; }

        public string? Name { get; set; }
        public string? Url { get; set; }
        public string? Profile { get; set; }
        public string? Politics { get; set; }

        public Guid? UserId { get; set; }
        public User? User { get; set; }

        public override IList<Product>? Items { get; set; }

        public string? GetLogo() => $"https://localhost:5000/uploads/stores/{Id}/logo.jpg";

        public string? GetBanner() => $"https://localhost:5000/uploads/stores/{Id}/banner.jpg";
    }
}