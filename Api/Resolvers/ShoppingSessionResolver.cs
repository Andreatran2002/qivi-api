using System;
using Core.Entities;
using Core.Repositories;

namespace Api.Resolvers
{
	[ExtendObjectType(typeof(ShoppingSession))]

	public class ShoppingSessionResolver
	{
		public ShoppingSessionResolver()
		{
		}
		
		public Task<ShoppingSession> GetShoppingSessionByCartAsync(
			  [Parent] CartItem cartItem,
			  [Service] IShoppingSessionRepository shoppingSessionRepository) => shoppingSessionRepository.GetByIdAsync(cartItem.SessionId);
        public Task<ShoppingSession> GetShoppingSessionByUserAsync(
              [Parent] ApplicationUser user,
              [Service] IShoppingSessionRepository shoppingSessionRepository) => shoppingSessionRepository.GetByUserId(user.Id.ToString());

    }
}

