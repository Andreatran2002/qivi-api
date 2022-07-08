using System;
using Core.Base;
using Core.Entities;

namespace Core.Repositories
{
	public interface IProductPriceRepository : IBaseRepository<ProductPrice>
	{
		public Task<IEnumerable<ProductPrice>> GetByProductId(string id);

	}
}

