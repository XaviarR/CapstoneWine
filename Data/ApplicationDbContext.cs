using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CapstoneWine.Models;

namespace CapstoneWine.Data
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}
        public DbSet<CapstoneWine.Models.SubscriptionsModel> Subscriptions { get; set; } = default!;

        public DbSet<CapstoneWine.Models.WinesModel> Wines { get; set; } = default!;

        public DbSet<CapstoneWine.Models.OrdersModel> Orders { get; set; } = default!;

        public DbSet<CapstoneWine.Models.CustomerModel> CustomerModel { get; set; } = default!;

        //Added code CB
        /*
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Relationship configurations
            // Configure the one-to-many relationship between AboutMe and User
             builder.Entity<AboutMe>()
                .HasOne(a => a.User) // AboutMe entity has one User
                .WithMany() // User entity can have multiple AboutMe entries
                .HasForeignKey(a => a.UserId); // Foreign key is UserId

            // Configure the one-to-many relationship between FoodIntake and User
            builder.Entity<FoodIntake>()
                .HasOne(f => f.User) // FoodIntake entity has one User
                .WithMany() // User entity can have multiple FoodIntake entries
                .HasForeignKey(f => f.UserId); // Foreign key is UserId 

            // Configure the one-to-one relationship between UserProfile and User
            builder.Entity<CustomerModel>()
                .HasOne(u => u.User) // UserProfile entity has one User
                .WithOne() // User entity can have only one UserProfile entry
                .HasForeignKey<UserProfile>(u => u.UserId); // Foreign key is UserId
    }
        */
    }


}