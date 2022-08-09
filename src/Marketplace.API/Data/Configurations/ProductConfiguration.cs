using Marketplace.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.API.Data.Configurations
{
    public static class ProductConfiguration
    {
        public static void ConfigureProduct(this ModelBuilder mb)
        {
            mb.Entity<Product>()
              .HasKey(p => p.Id)
              .Metadata
              .IsPrimaryKey();

            mb.Entity<Product>()
              .Property(p => p.Id)
              .ValueGeneratedOnAdd();
            
            mb.Entity<Product>()
              .Property(p => p.Name)
              .HasMaxLength(255)
              .IsRequired();
            
            mb.Entity<Product>()
              .Property(p => p.Description)
              .HasMaxLength(2500)
              .IsRequired();
            
            mb.Entity<Product>()
              .Property(p => p.Price)
              .HasPrecision(10, 2)
              .IsRequired();
        }
    }
}