namespace Amiasea.Enterprise.Data.Core.Entity;

public class Cart
{
    public Guid Id { get; set; }

    public List<CartItem> Items { get; set; } = [];
}