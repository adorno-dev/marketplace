using Marketplace.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.API.Data.Configurations
{
    public static class UserConfiguration
    {
        public static void ConfigureUser(this ModelBuilder mb) 
        {
            mb.Entity<User>()
              .HasMany(p => p.Reviews);
        }
    }
}