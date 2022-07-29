using System;
using Core.Entities;

namespace Core.Interfaces
{
	public interface IUserInformation
	{
        public Task<User> GetUserById(string userId);
    }
}

