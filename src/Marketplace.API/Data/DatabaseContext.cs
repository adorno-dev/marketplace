using Marketplace.API.Data.Configurations;
using Marketplace.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.API.Data
{
    public class DatabaseContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) {}

        public DbSet<Category> Categories => Set<Category>();

        public DbSet<Store> Stores => Set<Store>();

        public DbSet<Product> Products => Set<Product>();

        public DbSet<Review> Reviews => Set<Review>();

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.ConfigureCategory()
              .WithDataCategories();
              
            mb.ConfigureStore()
              .WithDataStores();

            mb.ConfigureProduct();
            mb.ConfigureUser();
            mb.ConfigureReview();

            base.OnModelCreating(mb);
        }
    }
}