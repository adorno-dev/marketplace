using Marketplace.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.API.Data.Configurations
{
    public static class CategoryConfiguration
    {
        public static void ConfigureCategory(this ModelBuilder mb)
        {
            mb.Entity<Category>()
              .HasKey(c => c.Id)
              .Metadata
              .IsPrimaryKey();

            mb.Entity<Category>()
              .Property(c => c.Id)
              .UseIdentityColumn(1000, 1);
            
            mb.Entity<Category>()
              .Property(c => c.Name)
              .HasMaxLength(255)
              .IsRequired();
            
            mb.Entity<Category>()
              .HasOne<Category>(c => c.Parent);
            
            mb.Entity<Category>()
              .HasMany<Category>(c => c.Categories);
        }        
    }
}