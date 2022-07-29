using System;
using Core.Entities;
using Core.Repositories;

namespace Api.Resolvers
{
	[ExtendObjectType(typeof(CartItemResolver))]

	public class CartItemResolver
	{
		public CartItemResolver()
		{
		}
        public Task<List<CartItem?>> GetCartsBySessionIdAsync(
         [Parent] ShoppingSession shoppingSession,
         [Service] ICartItemRepository cartItemRepository) => cartItemRepository.GetBySessionId(shoppingSession.Id);
	}
}

