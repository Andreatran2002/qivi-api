using System;
using Core.Entities;
using Core.Repositories;

namespace Api.Queries
{
	[ExtendObjectType(nameof(Query))]

	public class ProductPriceQuery
	{
		public ProductPriceQuery()
		{
		}
		public Task<IEnumerable<ProductPrice>> GetPriceByProductIdAsync(string productId, [Service] IProductPriceRepository productPriceRepository) =>
			productPriceRepository.GetByProductId(productId);

		
	}
}

