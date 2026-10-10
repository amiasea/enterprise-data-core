using Microsoft.EntityFrameworkCore;
using Amiasea.Enterprise.Data.Core.Entity;

namespace Amiasea.Enterprise.Data.Core;

public class CoreDbContext(DbContextOptions<CoreDbContext> options)
		: DbContext(options)
{
	public DbSet<Product> Products => Set<Product>();

	public DbSet<Cart> Carts => Set<Cart>();

	public DbSet<CartItem> CartItems => Set<CartItem>();

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.HasDefaultSchema("core");

		modelBuilder.ApplyConfigurationsFromAssembly(typeof(CoreDbContext).Assembly);
	}
}