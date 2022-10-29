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

        public DbSet<Favorite> Favorites => Set<Favorite>();

        public DbSet<Review> Reviews => Set<Review>();

        public DbSet<Cart> Carts => Set<Cart>();

        public DbSet<CartItem> CartItems => Set<CartItem>();

        public DbSet<Order> Orders => Set<Order>();

        public DbSet<OrderItem> OrderItems => Set<OrderItem>();

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.ConfigureCategory()
              .WithDataCategories();
              
            mb.ConfigureStore()
              .WithDataStores();

            mb.ConfigureProduct();
            mb.ConfigureUser();
            mb.ConfigureFavorite();
            mb.ConfigureReview();
            mb.ConfigureCart();
            mb.ConfigureCartItem();
            mb.ConfigureOrder();
            mb.ConfigureOrderItem();

            base.OnModelCreating(mb);
        }
    }
}