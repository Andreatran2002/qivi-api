using System;
using Core.Entities;
using Core.Repositories;
using HotChocolate.Subscriptions;

namespace Api.Mutations
{
	[ExtendObjectType(nameof(Mutation))]

	public class ProductPriceMutation 
	{
		public ProductPriceMutation()
		{
		}
        public async Task<ProductPrice> CreateProductPriceAsync(string sku, decimal price, string productId,


            [Service] IProductPriceRepository productPriceRepository, [Service] ITopicEventSender eventSender)
        {
            var result = await productPriceRepository.InsertAsync(new ProductPrice(sku,price,productId));


            return result;
        }

        public async Task<bool> RemoveProductPriceAsync(string id, [Service] IProductPriceRepository productPriceRepository, [Service] ITopicEventSender eventSender)
        {
            var result = await productPriceRepository.RemoveAsync(id);

           
            return result;
        }
    }
}

