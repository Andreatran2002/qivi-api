using System;
using Core.Entities;
using Core.Repositories;

namespace Api.Queries
{
	[ExtendObjectType(nameof(Query))]

	public class OrderDetailsQuery
	{
		public OrderDetailsQuery()
		{
		}
		public Task<IEnumerable<OrderDetails>> GetOrderssAsync([Service] IOrderDetailsRepository orderRepository) =>
			orderRepository.GetAllAsync();

		public async Task<OrderDetails> GetOrderDetailsById(string id, [Service] IOrderDetailsRepository orderRepository) =>
			await orderRepository.GetByIdAsync(id);
	
	}
}

