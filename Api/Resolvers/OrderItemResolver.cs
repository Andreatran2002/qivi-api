using System;
using Core.Entities;
using Core.Repositories;

namespace Api.Resolvers
{
	public class OrderItemResolver
	{
		public OrderItemResolver()
		{
		}
		public Task<IEnumerable<OrderItem>> GetOrderItemsByID(
		 [Parent] OrderDetails order,
		 [Service] IOrderItemRepository orderItemRepository) => orderItemRepository.GetByOrderId(order.Id);
	}
}

