using Marketplace.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.API.Data.Configurations
{
    public static class StoreConfiguration
    {
        public static void ConfigureStore(this ModelBuilder mb)
        {
            mb.Entity<Store>()
              .HasKey(c => c.Id)
              .Metadata
              .IsPrimaryKey();

            mb.Entity<Store>()
              .Property(c => c.Id)
              .UseIdentityColumn(1000, 1);
            
            mb.Entity<Store>()
              .Property(c => c.Name)
              .HasMaxLength(255)
              .IsRequired();
            
            mb.Entity<Store>()
              .Property(c => c.Joined)
              .HasDefaultValue<DateTime>(DateTime.UtcNow);
            
            mb.Entity<Store>()
              .Property(c => c.Categories)
              .HasMaxLength(255)
              .IsRequired();
        }
    }
}