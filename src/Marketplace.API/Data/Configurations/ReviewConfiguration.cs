using Marketplace.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.API.Data.Configurations
{
    public static class ReviewConfiguration
    {
        public static void ConfigureReview(this ModelBuilder mb)
        {
            mb.Entity<Review>()
              .HasKey(p => p.Id)
              .Metadata
              .IsPrimaryKey();

            mb.Entity<Review>()
              .Property(p => p.Posted)
              .HasDefaultValue(DateTime.UtcNow)
              .ValueGeneratedOnAddOrUpdate();

            mb.Entity<Review>()
              .HasOne(p => p.User);
        }
    }
}