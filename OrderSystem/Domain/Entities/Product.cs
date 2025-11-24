namespace OrderSystem.Domain.Entities;

public class Product
{
    public int Id { get; }
    public string Name { get; }
    public decimal Price { get; }

    public Product(int id, string name, decimal price)
    {
        Id = id;
        Name = name ?? string.Empty;
        Price = price;
    }
}
