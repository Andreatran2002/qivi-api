using System;
using Core.Entities;
using Core.Repositories;

namespace Api.Queries
{
	[ExtendObjectType(nameof(Query))]
	public class OrderItemQuery
	{
		public OrderItemQuery()
		{
		}
		public async Task<IEnumerable<OrderItem>> GetItemsByOrderId(string orderId, [Service] IOrderItemRepository orderItemRepository)
		=> await orderItemRepository.GetByOrderId(orderId);
		public async Task<OrderItem> GetItemById(string id, [Service] IOrderItemRepository orderItemRepository)
		=> await orderItemRepository.GetByIdAsync(id);

	}
}

