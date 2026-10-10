namespace Amiasea.Enterprise.Data.Core.Entity;

public class CartItem
{
    public Guid Id { get; set; }

    public Guid CartId { get; set; }

    public Guid ProductId { get; set; }

    public int Quantity { get; set; }
}