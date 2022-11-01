using Marketplace.API.Models;
using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marketplace.API.Data.Configurations
{
    public static class UserConfiguration
    {
        public static void ConfigureUser(this ModelBuilder mb) 
        {
            mb.Entity<User>()
              .HasKey(p => p.Id)
              .Metadata
              .IsPrimaryKey();

            mb.Entity<User>()
              .HasMany(p => p.Reviews);

            mb.Entity<User>()
              .HasOne(s => s.Store);

            mb.Entity<User>()
              .Ignore(s => s.StoreId);

            mb.Entity<User>()
              .Ignore(c => c.CartId);

            mb.Entity<User>()
              .HasOne(u => u.Cart)
              .WithOne(u => u.User);
        }
    }

    // public class UserConfiguration : IEntityTypeConfiguration<User>
    // {
    //     public void Configure(EntityTypeBuilder<User> builder)
    //     {
	// 		builder.HasKey(p => p.Id)
	// 			   .Metadata
	// 			   .IsPrimaryKey();

	// 		builder.HasMany(p => p.Reviews);

	// 		builder.HasOne(s => s.Store);

	// 		builder.Ignore(s => s.StoreId);

	// 		builder.Ignore(c => c.CartId);

	// 		builder.HasOne(u => u.Cart)
	// 			   .WithOne(u => u.User);
    //     }
    // }
}