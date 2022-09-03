using System;
using Core.Base;

namespace Core.Entities
{
	public class ShoppingSession : BaseEntity
	{
		public ShoppingSession(string userInfoId, decimal total )
		{
            UserInfoId = userInfoId;
			Total = total;
		}
		public string UserInfoId { set; get; }
		public decimal Total { set; get; }
		public DateTime CreatedAt { set; get; } = DateTime.Now; 
		public DateTime ModifiedAt { set; get; } = DateTime.Now;
	}
}

