using Marketplace.API.Utils;

namespace Marketplace.API.Models
{
    public class Cart : Pagination<CartItem>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public User? User { get; set; }
        public override IList<CartItem>? Items { get; set; }
    }
}