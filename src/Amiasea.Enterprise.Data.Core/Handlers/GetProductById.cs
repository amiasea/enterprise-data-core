using Microsoft.EntityFrameworkCore;

using Amiasea.Enterprise.Contract.Core.Queries;
using Amiasea.Enterprise.Contract.Core.Types;
using Amiasea.Enterprise.Data.Core;

namespace Amiasea.Enterprise.Data.Core.Handlers;

public sealed class GetProductByIdQueryHandler(CoreDbContext db)
{
    public async Task<Product?> HandleAsync(
        GetProductByIdQuery query,
        CancellationToken cancellationToken)
    {
        return await db.Products
            .AsNoTracking()
            .Where(product => product.Id == query.Id)
            .Select(product => new Product(
                product.Id,
                product.Name,
                product.Price))
            .SingleOrDefaultAsync(cancellationToken);
    }
}