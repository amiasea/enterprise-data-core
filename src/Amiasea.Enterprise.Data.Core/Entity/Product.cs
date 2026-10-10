namespace Amiasea.Enterprise.Data.Core.Entity;

public class Product
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public decimal Price { get; set; }
}