using Marketplace.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.API.Data.Configurations
{
    public static class CartItemConfiguration
    {
        public static ModelBuilder ConfigureCartItem(this ModelBuilder mb)
        {
            mb.Entity<CartItem>()
              .HasKey(c => c.Id)
              .Metadata
              .IsPrimaryKey();
            
            mb.Entity<CartItem>()
              .HasIndex(c => new {c.CartId, c.ProductId})
              .IsUnique();

            // mb.Entity<CartItem>()
            //   .Property(p => p.Price)
            //   .HasColumnName("decimal(10,2)")
            //   .IsRequired();

            mb.Entity<CartItem>()
              .Ignore(c => c.StoreId);

            mb.Entity<CartItem>()
              .Ignore(c => c.Description);
            
            mb.Entity<CartItem>()
              .Ignore(c => c.Price);

            mb.Entity<CartItem>()
              .HasOne(c => c.Product);

            mb.Entity<CartItem>()
              .HasOne(c => c.Cart)            
              .WithMany(c => c.Items)
              .HasForeignKey(c => c.CartId);

            return mb;
        }
    }
}