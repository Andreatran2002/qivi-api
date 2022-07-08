using System;
using Core.Base;

namespace Core.Entities
{
	public class CartItem : BaseEntity
	{
		public CartItem()
        {

        }
		public CartItem(string priceId , int quantity , string sessionId)
		{
			PriceId = priceId;
			Quantity = quantity;
			SessionId = sessionId; 
		}
		public string SessionId { set; get; }
		public string PriceId { set; get; }
		public int Quantity { set; get; }
		public DateTime CreatedAt { set; get; } = DateTime.Now;
		public DateTime ModifiedAt { set; get; } = DateTime.Now; 


	}
}

