using System;
using System.Collections.Generic;
using System.Linq; // обязательно
using OrderSystem.Domain.Delivery;

namespace OrderSystem.Domain.Entities;

public enum OrderStatus { Created, Paid, Packed, Shipped, Delivered, Cancelled }

// Ограничение TDelivery : Delivery обязательно — иначе нет гарантии, что у TDelivery есть Address
public class Order<TDelivery, TStruct>
    where TDelivery : OrderSystem.Domain.Delivery.Delivery
    where TStruct : struct
{
    private readonly List<OrderItem> _items = new();

    public int Number { get; }
    public string Description { get; set; }
    public TDelivery Delivery { get; set; }
    public TStruct Metadata { get; set; }
    public OrderStatus Status { get; private set; } = OrderStatus.Created;
    public IReadOnlyList<OrderItem> Items => _items.AsReadOnly();

    public Order(int number, TDelivery delivery, string description)
    {
        Number = number;
        Delivery = delivery ?? throw new ArgumentNullException(nameof(delivery));
        Description = description ?? string.Empty;
    }

    public void AddItem(Product product, int qty)
    {
        if (product == null) throw new ArgumentNullException(nameof(product));
        if (qty <= 0) throw new ArgumentOutOfRangeException(nameof(qty));

        var existing = _items.FirstOrDefault(i => i.Product.Id == product.Id);
        if (existing != null)
            existing.Increase(qty);
        else
            _items.Add(new OrderItem(product, qty));
    }

    public void RemoveItem(int productId)
    {
        var item = _items.FirstOrDefault(i => i.Product.Id == productId)
                   ?? throw new InvalidOperationException("Item not found");
        _items.Remove(item);
    }

    public decimal TotalPrice => _items.Sum(i => i.TotalPrice);

    public void DisplayAddress()
    {
        if (Delivery == null)
        {
            Console.WriteLine("Delivery is not set");
            return;
        }

        Console.WriteLine(Delivery.Address?.ToString() ?? "Address not set");
    }

    public void SetStatus(OrderStatus status) => Status = status;
}
