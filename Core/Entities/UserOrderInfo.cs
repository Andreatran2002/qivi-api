using System;
using Core.Base;

namespace Core.Entities
{
	public class UserOrderInfo :  BaseEntity
	{
		public UserOrderInfo(string userId, string recipient , string phoneNumber, string address)
		{
			UserId = userId;
			Recipient = recipient;
			PhoneNumber = phoneNumber;
			Address = address;
		}
		public string UserId { set; get; }
        public string Recipient { set; get; }
        public string PhoneNumber { set; get; }
        public string Address { set; get; }
    }
}

