using Microsoft.AspNetCore.Identity;

namespace Marketplace.API.Models
{
    public class User : IdentityUser<Guid> 
    {
        public ICollection<Review>? Reviews { get; set; }

        public Guid? StoreId { get; set; }
        public Guid? CartId { get; set; }

        public Store? Store { get; set; }
        public Cart? Cart { get; set; }
    }
}