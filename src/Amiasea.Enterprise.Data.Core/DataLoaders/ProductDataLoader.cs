using GreenDonut;
using Microsoft.EntityFrameworkCore;
using Amiasea.Enterprise.Data.Core.Entity;

namespace Amiasea.Enterprise.Data.Core.DataLoader;

internal static class ProductDataLoaders
{
	[DataLoader]
	public static async Task<Dictionary<Guid, Product>>
		GetProductByIdAsync(
			IReadOnlyList<Guid> ids,
			CoreDbContext db,
			CancellationToken cancellationToken)
	{
		var products = await db.Products
			.AsNoTracking()
			.Where(product => ids.Contains(product.Id))
			.ToDictionaryAsync(product => product.Id, cancellationToken);

		return products;
	}
}