using Marketplace.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.API.Data.Configurations
{
    public static class OrderItemConfiguration
    {
        public static ModelBuilder ConfigureOrderItem(this ModelBuilder mb)
        {
            mb.Entity<OrderItem>()
              .HasKey(o => o.Id)
              .Metadata
              .IsPrimaryKey();

            // mb.Entity<OrderItem>()
            //   .HasIndex(o => o.OrderId)
            //   .IsUnique(false);

            // mb.Entity<OrderItem>()
            //   .HasIndex(f => f.ProductId)
            //   .IsUnique(false);

            // mb.Entity<OrderItem>()
            //   .HasIndex(c => new {c.OrderId, c.ProductId})
            //   .IsUnique();

            mb.Entity<OrderItem>()
              .Property(o => o.Price)
              .HasPrecision(10, 2)
              .IsRequired();

            mb.Entity<OrderItem>()
              .HasOne(o => o.Product);

            return mb;
        }
    }
}