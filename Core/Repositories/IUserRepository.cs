using System;
using Core.Base;
using Core.Entities;

namespace Core.Repositories
{
	public interface IUserRepository : IBaseRepository<User>
	{

		public Task<User> GetUserByPhoneNumber(string phoneNumber);
		public Task<User> GetUserByName(string name); 
		public Task<bool> AccountInfoIsAvailable(string name, string phoneNumber);
        public  Task<List<User>?> GetPossibleUserName(string possibleUsername);

    }
}

