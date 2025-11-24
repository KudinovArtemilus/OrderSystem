using OrderSystem.Domain.ValueObjects;

namespace OrderSystem.Domain.Delivery;

public class HomeDelivery : Delivery
{
    public string? CourierCompany { get; set; }

    public override string TypeName => "HomeDelivery";

    public override string GetDeliveryInfo() => $"Home delivery by {CourierCompany ?? "unknown"} to: {Address?.ToString() ?? "no address"}";
}
