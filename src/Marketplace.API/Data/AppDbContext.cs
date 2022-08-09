using Marketplace.API.Data.Configurations;
using Marketplace.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<Category> Categories => Set<Category>();

        public DbSet<Store> Stores => Set<Store>();

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.ConfigureCategory();
            mb.ConfigureStore();
        }
    }
}