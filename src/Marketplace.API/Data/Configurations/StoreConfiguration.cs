using Marketplace.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.API.Data.Configurations
{
    public static class StoreConfiguration
    {
        public static ModelBuilder ConfigureStore(this ModelBuilder mb)
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
            
            // mb.Entity<Store>()
            //   .HasOne<User>(p => p.User)
            //   .WithOne(p => p.Store)
            //   .HasPrincipalKey<Store>(p => p.UserId);

            return mb;
        }

        public static void WithDataStores(this ModelBuilder mb)
        {
            // var data = new Store 
            // {
            //     Id = Guid.NewGuid(),
            //     Name = "A4U Store",
            //     // UserId = Guid.Parse("62f95015-1f7e-4f55-0fa2-08da7d3a1034"), // Defined to default user
            //     Categories = "1026"
            // };

            // mb.Entity<Store>()
            //   .HasData(data);
        }
    }
}