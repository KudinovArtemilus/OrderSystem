using OrderSystem.Domain.ValueObjects;

namespace OrderSystem.Domain.Delivery
{
    public class ShopDelivery : Delivery
    {
        public string ShopId { get; set; }

        public override string TypeName => "ShopDelivery";

        public override string GetDeliveryInfo()
        {
            return $"Pickup in shop ID: {ShopId}. Address: {Address}";
        }
    }
}
