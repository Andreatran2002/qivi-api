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
        public Task<ApplicationUser> GetUserByShoppingSessionAsync(
             [Parent] ShoppingSession shoppingSession,
             [Service] IUserRepository repo) => repo.GetByIdAsync(shoppingSession.UserId);
        public Task<ApplicationUser> GetUserByOrderAsync(
            [Parent] OrderDetails order,
            [Service] IUserRepository repo) => repo.GetByIdAsync(order.UserId);
        public async Task<ApplicationUser> GetUserInfoAsync(
               [Parent] UserOrderInfo userOrderInfo,
               [Service] IUserRepository repo) => await repo.GetByIdAsync(userOrderInfo.UserId.ToString());


    }
}

