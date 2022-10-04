using Marketplace.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.API.Data.Configurations
{
    public static class CartConfiguration
    {
        public static ModelBuilder ConfigureCart(this ModelBuilder mb)
        {
            mb.Entity<Cart>()
              .HasKey(c => c.Id)
              .Metadata
              .IsPrimaryKey();
            
            mb.Entity<Cart>()
              .HasOne(c => c.User)
              .WithOne(c => c.Cart)
              .HasForeignKey<Cart>(c => c.UserId);

            mb.Entity<Cart>()
              .Ignore(c => c.PageCount);
            
            mb.Entity<Cart>()
              .Ignore(c => c.PageIndex);

            mb.Entity<Cart>()
              .Ignore(c => c.PageSize);

            mb.Entity<Cart>()
              .Ignore(c => c.TotalItems);

            mb.Entity<Cart>()
              .HasMany(c => c.Items);

            return mb;
        }
    }
}