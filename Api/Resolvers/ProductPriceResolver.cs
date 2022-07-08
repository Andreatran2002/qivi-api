using System;
using Core.Entities;
using Core.Repositories;

namespace Api.Resolvers
{
    [ExtendObjectType(typeof(ProductPrice))]
	public class ProductPriceResolver
	{
		public  Task<ProductPrice> GetPriceInOrderAsync(
		 [Parent] OrderItem order, [Service] IProductPriceRepository priceRepository)
		=>  priceRepository.GetByIdAsync(order.PriceId);

		public  Task<ProductPrice> GetPriceInCartAsync(
		 [Parent] CartItem cart, [Service] IProductPriceRepository priceRepository)
		=>  priceRepository.GetByIdAsync(cart.PriceId);


		public Task<IEnumerable<ProductPrice>> GetPricesByProductIdAsync(
				 [Parent] Product product,
				 [Service] IProductPriceRepository priceRepository) => priceRepository.GetByProductId(product.Id);

	}
}

