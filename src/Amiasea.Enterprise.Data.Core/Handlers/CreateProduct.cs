using Amiasea.Enterprise.Contract.Core.Commands;
using Amiasea.Enterprise.Data.Core.Entity;

namespace Amiasea.Enterprise.Data.Core.Handlers;

public sealed class CreateProductCommandHandler(
    CoreDbContext db)
{
    public async Task<Product> HandleAsync(
        CreateProductCommand command,
        CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            Price = command.Price
        };

        db.Products.Add(product);
        await db.SaveChangesAsync(cancellationToken);

        return product;
    }
}