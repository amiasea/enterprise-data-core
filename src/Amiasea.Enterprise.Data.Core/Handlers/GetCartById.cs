using Microsoft.EntityFrameworkCore;

using Amiasea.Enterprise.Contract.Core.Queries;
using Amiasea.Enterprise.Contract.Core.Types;
using Amiasea.Enterprise.Data.Core;

using CartContract = Amiasea.Enterprise.Contract.Core.Types.Cart;
using CartItemContract = Amiasea.Enterprise.Contract.Core.Types.CartItem;
using ProductContract = Amiasea.Enterprise.Contract.Core.Types.Product;

namespace Amiasea.Enterprise.Data.Core.Handlers;

public sealed class GetCartByIdQueryHandler(CoreDbContext db)
{
    public async Task<CartContract?> HandleAsync(
        GetCartByIdQuery query,
        CancellationToken cancellationToken)
    {
        var cart = await db.Carts
            .AsNoTracking()
            .Include(cart => cart.Items)
            .SingleOrDefaultAsync(
                cart => cart.Id == query.Id,
                cancellationToken);

        if (cart is null)
        {
            return null;
        }

        var productIds = cart.Items
            .Select(item => item.ProductId)
            .Distinct()
            .ToArray();

        var products = await db.Products
            .AsNoTracking()
            .Where(product => productIds.Contains(product.Id))
            .Select(product => new ProductContract(
                product.Id,
                product.Name,
                product.Price))
            .ToDictionaryAsync(
                product => product.Id,
                cancellationToken);

        var items = cart.Items
            .Select(item =>
            {
                if (!products.TryGetValue(item.ProductId, out var product))
                {
                    throw new InvalidOperationException(
                        $"Product '{item.ProductId}' referenced by cart '{cart.Id}' was not found.");
                }

                return new CartItemContract(product, item.Quantity);
            })
            .ToArray();

        return new CartContract(cart.Id, items);
    }
}