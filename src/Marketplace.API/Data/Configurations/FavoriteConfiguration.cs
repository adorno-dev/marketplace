using Marketplace.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.API.Data.Configurations
{
    public static class FavoriteConfiguration
    {
        public static ModelBuilder ConfigureFavorite(this ModelBuilder mb)
        {
            mb.Entity<Favorite>()
              .ToTable("Favorites");

            mb.Entity<Favorite>()
              .HasIndex(f => f.UserId)
              .IsUnique(false);

            mb.Entity<Favorite>()
              .HasIndex(f => f.ProductId)
              .IsUnique(false);

            mb.Entity<Favorite>()
              .HasKey(f => new { f.UserId, f.ProductId });
            
            mb.Entity<Favorite>()
              .HasOne(f => f.User);
            
            mb.Entity<Favorite>()
              .HasOne(f => f.Product);

            return mb;
        }
    }
}