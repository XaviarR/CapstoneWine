using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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
    }
}