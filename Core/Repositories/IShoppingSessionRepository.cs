using System;
using Core.Base;
using Core.Entities;
using Core.Interfaces;

namespace Core.Repositories
{
	public interface IShoppingSessionRepository : IBaseRepository<ShoppingSession>
	{
		public Task<ShoppingSession> GetByUserId(string userId);

	}
}

