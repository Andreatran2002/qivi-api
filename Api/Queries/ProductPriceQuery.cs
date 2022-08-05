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
        [UsePaging(MaxPageSize = 30)]
        [UseFiltering]
        [UseSorting]
        public Task<IEnumerable<ProductPrice>> GetPriceAndProductAsync([Service] IProductPriceRepository productPriceRepository) =>
			productPriceRepository.GetAllAsync();

        public Task<IEnumerable<ProductPrice>> GetPriceByProductIdAsync(string productId, [Service] IProductPriceRepository productPriceRepository) =>
			productPriceRepository.GetByProductId(productId);

		
	}
}

