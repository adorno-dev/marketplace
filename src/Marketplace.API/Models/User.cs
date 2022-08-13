using Microsoft.AspNetCore.Identity;

namespace Marketplace.API.Models
{
    public class User : IdentityUser<Guid> 
    {
        public ICollection<Review>? Reviews { get; set; }

        public Guid StoreId { get; set; }
        public Store? Store { get; set; }
    }
}