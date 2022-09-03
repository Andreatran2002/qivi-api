using System;
using Core.Entities;
using Core.Repositories;

namespace Api.Resolvers
{
	[ExtendObjectType(typeof(OrderDetails))]

	public class OrderDetailsResolver
	{
		public OrderDetailsResolver()
		{
		}
		public Task<OrderDetails?> GetOrderDetailsByItems(
		  [Parent] OrderItem order,
		  [Service] IOrderDetailsRepository orderDetailsRepository) => orderDetailsRepository.GetByIdAsync(order.OrderId);
        public async Task<IEnumerable<OrderDetails>> GetOrderDetailsByUserId(
          [Parent] ApplicationUser user,
          [Service] IOrderDetailsRepository orderDetailsRepository) => await orderDetailsRepository.GetByUserId(user.Id.ToString());

    }
}

