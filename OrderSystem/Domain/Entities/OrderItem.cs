namespace OrderSystem.Domain.Entities;

public class OrderItem
{
    public Product Product { get; }
    public int Quantity { get; private set; }

    public decimal TotalPrice => Product.Price * Quantity;

    public OrderItem(Product product, int quantity)
    {
        Product = product ?? throw new ArgumentNullException(nameof(product));
        if (quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity));
        Quantity = quantity;
    }

    public void Increase(int amount)
    {
        if (amount <= 0) throw new ArgumentOutOfRangeException(nameof(amount));
        Quantity += amount;
    }

    public void Decrease(int amount)
    {
        if (amount <= 0) throw new ArgumentOutOfRangeException(nameof(amount));
        Quantity -= amount;
        if (Quantity <= 0) throw new InvalidOperationException("Quantity must remain > 0");
    }
}
