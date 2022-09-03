using System;
using Core.Base;
using Core.Entities.Enum;

namespace Core.Entities
{
	public class OrderDetails : BaseEntity
	{
		public OrderDetails(string userInfoId , string userId)
		{
            UserInfoId = userInfoId;
			UserId = userId;
		}
		public OrderStatus Status { set; get; } = OrderStatus.PENDING;
		public string UserInfoId { set; get; } 
		public string UserId { set; get; } 
		public decimal Total { set; get; } = 0; 
		public DateTime CreatedAt { set; get; } = DateTime.Now;
		public DateTime ModifiedAt { set; get; } = DateTime.Now;
	}

    
}


