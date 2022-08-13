using Marketplace.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.API.Data.Configurations
{
    public static class CategoryConfiguration
    {
        public static ModelBuilder ConfigureCategory(this ModelBuilder mb)
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
            
            return mb;
        }

        public static void WithData(this ModelBuilder mb)
        {
            var data = new List<Category>()
            {
                new Category { Name = "Animals" },
                new Category { Name = "Animated Objects" },
                new Category { Name = "Animations" },
                new Category { Name = "Apparel" },
                new Category { Name = "Art" },
                new Category { Name = "Audio and Video" },
                new Category { Name = "Avatar Accessories" },
                new Category { Name = "Avatar Appearance" },
                new Category { Name = "Avatar Components" },
                new Category { Name = "Breedables" },
                new Category { Name = "Building and Object Components" },
                new Category { Name = "Buildings and Other Structures" },
                new Category { Name = "Business" },
                new Category { Name = "Celebrations" },
                new Category { Name = "Complete Avatars" },
                new Category { Name = "Furry" },
                new Category { Name = "Gachas" },
                new Category { Name = "Gadgets" },
                new Category { Name = "Home and Garden" },
                new Category { Name = "Miscellaneous" },
                new Category { Name = "Real Estate" },
                new Category { Name = "Recreation and Entertainment" },
                new Category { Name = "Scripts" },
                new Category { Name = "Services" },
                new Category { Name = "Used Items" },
                new Category { Name = "Vehicles" },
                new Category { Name = "Weapons" },
                new Category { Name = "Everything Else" }
            };

            mb.Entity<Category>()
              .HasData(data);
        }
    }
}