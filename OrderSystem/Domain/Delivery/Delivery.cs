using OrderSystem.Domain.ValueObjects;

namespace OrderSystem.Domain.Delivery;

public abstract class Delivery
{
    public Address? Address { get; protected set; } // nullable — на время создания заказа может быть пусто

    public abstract string TypeName { get; }

    public virtual void SetAddress(Address address) => Address = address;

    public abstract string GetDeliveryInfo();
}
