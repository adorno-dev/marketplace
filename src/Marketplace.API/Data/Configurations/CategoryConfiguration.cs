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
              .UseIdentityColumn(1000, 1)
              .ValueGeneratedOnAdd();
            
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

        public static void WithDataCategories(this ModelBuilder mb)
        {
            var data = new List<Category>()
            {
                new Category { Id = 1000, Name = "Animals" },
                new Category { Id = 1001, Name = "Animated Objects" },
                new Category { Id = 1002, Name = "Animations" },
                new Category { Id = 1003, Name = "Apparel" },
                new Category { Id = 1004, Name = "Art" },
                new Category { Id = 1005, Name = "Audio and Video" },
                new Category { Id = 1006, Name = "Avatar Accessories" },
                new Category { Id = 1007, Name = "Avatar Appearance" },
                new Category { Id = 1008, Name = "Avatar Components" },
                new Category { Id = 1009, Name = "Breedables" },
                new Category { Id = 1010, Name = "Building and Object Components" },
                new Category { Id = 1011, Name = "Buildings and Other Structures" },
                new Category { Id = 1012, Name = "Business" },
                new Category { Id = 1013, Name = "Celebrations" },
                new Category { Id = 1014, Name = "Complete Avatars" },
                new Category { Id = 1015, Name = "Furry" },
                new Category { Id = 1016, Name = "Gachas" },
                new Category { Id = 1017, Name = "Gadgets" },
                new Category { Id = 1018, Name = "Home and Garden" },
                new Category { Id = 1019, Name = "Miscellaneous" },
                new Category { Id = 1020, Name = "Real Estate" },
                new Category { Id = 1021, Name = "Recreation and Entertainment" },
                new Category { Id = 1022, Name = "Scripts" },
                new Category { Id = 1023, Name = "Services" },
                new Category { Id = 1024, Name = "Used Items" },
                new Category { Id = 1025, Name = "Vehicles" },
                new Category { Id = 1026, Name = "Weapons" },
                new Category { Id = 1027, Name = "Everything Else" }
            };

            mb.Entity<Category>()
              .HasData(data);
        }
    }
}