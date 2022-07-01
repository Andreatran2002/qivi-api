using System;
using Core.Entities;
using Core.Repositories;

namespace Api.Resolvers
{
	[ExtendObjectType(typeof(User))]

	public class UserResolver
	{
		public UserResolver()
		{
		}
		public Task<User> GetUserInfoAsync(
			  [Parent] Bill bill,
			  [Service] IUserRepository userRepository) => userRepository.GetByIdAsync(bill.CustomerId);

		public Task<User> GetUserByShoppingSessionAsync(
			  [Parent] ShoppingSession shoppingSession,
			  [Service] IUserRepository userRepository) => userRepository.GetByIdAsync(shoppingSession.UserId);
		public Task<User> GetUserByOrderAsync(
			  [Parent] OrderDetails order,
			  [Service] IUserRepository userRepository) => userRepository.GetByIdAsync(order.UserId);


	}
}

