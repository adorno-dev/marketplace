using Marketplace.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.API.Data.Configurations
{
    public static class UserConfiguration
    {
        public static void ConfigureUser(this ModelBuilder mb) 
        {
            mb.Entity<User>()
              .HasKey(p => p.Id)
              .Metadata
              .IsPrimaryKey();

            mb.Entity<User>()
              .HasMany(p => p.Reviews);
            
            mb.Entity<User>()
              .HasOne(s => s.Store);
            
            mb.Entity<User>()
              .HasOne(u => u.Cart)
              .WithOne(u => u.User)
              .HasForeignKey<User>(u => u.CartId);
        }
    }
}