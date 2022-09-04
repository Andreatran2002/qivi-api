using System;
using Core.Base;

namespace Core.Entities
{
	public class ShoppingSession : BaseEntity
	{
		public ShoppingSession(string userId, decimal total )
		{
            UserId = userId;
			Total = total;
		}
		public string UserId { set; get; }
		public decimal Total { set; get; }
		public DateTime CreatedAt { set; get; } = DateTime.Now; 
		public DateTime ModifiedAt { set; get; } = DateTime.Now;
	}
}

