using GreenDonut.Data;
using HotChocolate.Types.Pagination;
using Microsoft.EntityFrameworkCore;

using Amiasea.Enterprise.Contract.Core.Queries;
using Amiasea.Enterprise.Contract.Core.Types;

namespace Amiasea.Enterprise.Data.Core.Handlers;

public sealed class GetProductsQueryHandler(CoreDbContext db)
{
  public async Task<PageConnection<Product>> HandleAsync(
      GetProductsQuery query,
      CancellationToken cancellationToken)
  {
    return await db.Products
        .AsNoTracking()
        .OrderBy(product => product.Id)
        .Select(product => new Product(
            product.Id,
            product.Name,
            product.Price))
        .ToPageAsync(query.Args, cancellationToken);
  }
}