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
        public IEnumerable<UserOrderInfo> GetUserInfoAsync(
              [Parent] User user,
              [Service] IUserOrderInfoRepository repo) => repo.GetByUserId(user.Id);

    }
}

