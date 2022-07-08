using System;
using Core.Base;

namespace Core.Entities
{
	public class OrderItem : BaseEntity
	{
		public OrderItem(string orderId, string priceId, int quantity)
		{
			OrderId = orderId;
			PriceId = priceId;
			Quantity = quantity;

		}
		public string OrderId { set; get; }
		public string PriceId { set; get; }
		public int Quantity { set; get; }
		public DateTime CreatedAt { set; get; } = DateTime.Now;
		public DateTime ModifiedAt { set; get; } = DateTime.Now; 
	}
}

