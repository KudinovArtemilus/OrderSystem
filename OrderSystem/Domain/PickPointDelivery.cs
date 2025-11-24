using OrderSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using OrderSystem.Domain.Delivery;

namespace OrderSystem.Domain.Delivery
{
    public class PickPointDelivery : Delivery
    {
        public string PickupCompany { get; set; }
        public string PickupPointId { get; set; }

        public override string TypeName => "PickPointDelivery";

        public override string GetDeliveryInfo()
        {
            return $"Pickpoint {PickupCompany}, point #{PickupPointId}. Address: {Address}";
        }
    }
}
