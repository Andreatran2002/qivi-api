using System;
using Core.Base;
using Core.Entities;

namespace Core.Repositories
{
	public interface IProductRepository : IBaseRepository<Product>
	{
		public Task<IEnumerable<Product>> GetByDiscountId(string id);
		public Task<IEnumerable<Product>> GetByCategoryId(string id);

	}
}

