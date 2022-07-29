using System;
using Core.Entities;

namespace Api.Queries
{
    [ExtendObjectType(nameof(Query))]

    public class UserOrderInfoQuery
	{
		public UserOrderInfoQuery()
		{
		}
        //public Task<IEnumerable<UserOrderInfo>> GetOrderInfoAsync([Service] IShoppingSessionRepository shoppingRepository) =>
        //    shoppingRepository.GetAllAsync();

    }
}

