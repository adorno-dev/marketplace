using Marketplace.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.API.Data.Configurations
{
    public static class OrderConfiguration
    {
        public static ModelBuilder ConfigureOrder(this ModelBuilder mb)        
        {
            mb.Entity<Order>()
              .HasKey(o => o.Id)
              .Metadata
              .IsPrimaryKey();
            
            mb.Entity<Order>()
              .HasOne(o => o.User);

            mb.Entity<Order>()
              .Property(o => o.CartId)
              .IsRequired();
            
            mb.Entity<Order>()
              .HasMany(o => o.Items);

            return mb;
        }
    }
}