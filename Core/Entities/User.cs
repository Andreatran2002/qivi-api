using System;
using Core.Base;

namespace Core.Entities
{
	public class User : BaseEntity
	{
		public User()
        {

        }
		public User(string name,string firstName, string lastName,string phoneNumber)
		{
			UserName = name;
			FirstName = firstName;
			LastName = lastName; 
			PhoneNumber = phoneNumber;
		}
		public string UserName { set; get; }
		public string FirstName { set; get; }
		public string LastName { set; get; }
		public string PhoneNumber { set; get; }

		
	}
}

