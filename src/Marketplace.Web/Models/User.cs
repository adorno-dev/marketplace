using Microsoft.AspNetCore.Identity;

namespace Marketplace.Web.Models
{
    public class User : IdentityUser<Guid>
    {
        public Guid? StoreId { get; set; }
        public Store? Store { get; set; }
    }
}