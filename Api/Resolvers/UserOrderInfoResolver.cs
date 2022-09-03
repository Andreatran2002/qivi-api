using System;
using Core.Entities;
using Core.Repositories;

namespace Api.Resolvers
{
    [ExtendObjectType(typeof(UserOrderInfo))]

    public class UserOrderInfoResolver
	{
		public UserOrderInfoResolver()
		{
		}
        public Task<UserOrderInfo> GetUserByOrderAsync(
              [Parent] OrderDetails order,
              [Service] IUserOrderInfoRepository repo) => repo.GetByIdAsync(order.UserInfoId);
        public Task<UserOrderInfo> GetUserByShoppingSessionAsync(
              [Parent] ShoppingSession shoppingSession,
              [Service] IUserOrderInfoRepository repo) => repo.GetByIdAsync(shoppingSession.UserInfoId);
        public IEnumerable<UserOrderInfo> GetUserInfo(
              [Parent] ApplicationUser user,
              [Service] IUserOrderInfoRepository repo) => repo.GetByUserId(user.Id.ToString());

    }
}

