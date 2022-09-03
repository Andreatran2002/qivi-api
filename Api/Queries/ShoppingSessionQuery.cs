using System;
using Core.Entities;
using Core.Repositories;

namespace Api.Queries
{
	[ExtendObjectType(nameof(Query))]
	public class ShoppingSessionQuery
	{
		public ShoppingSessionQuery()
		{
		}
		public Task<IEnumerable<ShoppingSession>> GetShoppingSessionsAsync([Service] IShoppingSessionRepository shoppingRepository) =>
			shoppingRepository.GetAllAsync();

		public Task<ShoppingSession> GetShoppingSessionById(string id, [Service] IShoppingSessionRepository shoppingRepository) =>
			shoppingRepository.GetByIdAsync(id);
		public Task<ShoppingSession> GetShoppingSessionByUserId(string id, [Service] IShoppingSessionRepository shoppingRepository) =>
			shoppingRepository.GetByUserId(id);
	}
}

