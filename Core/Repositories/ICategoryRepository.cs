using System;
using Core.Base;
using Core.Entities;

namespace Core.Repositories
{
	public interface ICategoryRepository : IBaseRepository<Category>
	{
		public Task<Category> GetByCategoryId(string id);
	}

	
}

