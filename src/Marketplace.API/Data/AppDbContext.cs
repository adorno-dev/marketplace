using Marketplace.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<Category> Categories => Set<Category>();

        protected override void OnModelCreating(ModelBuilder mb)
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