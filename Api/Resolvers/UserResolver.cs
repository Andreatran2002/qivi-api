using System;
using Core.Entities;
using Core.Repositories;

namespace Api.Resolvers
{
	[ExtendObjectType(typeof(ApplicationUser))]

	public class UserResolver
	{
		public UserResolver()
		{
		}
        public async Task<ApplicationUser> GetUserInfoAsync(
               [Parent] UserOrderInfo userOrderInfo,
               [Service] IUserRepository repo) => await repo.GetByIdAsync(userOrderInfo.UserId.ToString());


    }
}

