using System;
namespace Core.Entities.Enum
{
	public enum OrderStatus {
        PENDING,
        AWAITING_PAYMENT,
        AWAITING_FULLFILLMENT,
        AWAITING_PICKUP,
        AWAITING_SHIPMENT,
        SHIPPED,
        COMPLETED,
        CANCELLED,
    }
}

