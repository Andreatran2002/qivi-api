using System;
using Core.Base;
using Core.Entities;
namespace Core.Repositories
{
	public interface IOrderItemRepository : IBaseRepository<OrderItem>
	{
		public Task<IEnumerable<OrderItem>> GetByOrderId(string orderId);

	}
}

