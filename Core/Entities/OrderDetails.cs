using System;
using Core.Base;

namespace Core.Entities
{
	public class OrderDetails : BaseEntity
	{
		public OrderDetails(string userId )
		{
			UserId = userId;
		}
		public string Status { set; get; } = "";
		public string UserId { set; get; } 
		public decimal Total { set; get; } = 0; 
		public DateTime CreatedAt { set; get; } = DateTime.Now;
		public DateTime ModifiedAt { set; get; } = DateTime.Now;
	}
}

