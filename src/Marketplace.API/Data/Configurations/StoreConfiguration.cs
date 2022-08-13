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
              .Property(c => c.Name)
              .HasMaxLength(255)
              .IsRequired();
            
            mb.Entity<Store>()
              .Property(c => c.Categories)
              .HasMaxLength(255)
              .IsRequired();
            
            mb.Entity<Store>()
              .HasOne<User>(p => p.User)
              .WithOne(p => p.Store)
              .HasForeignKey<User>(p => p.StoreId);
        }
    }
}