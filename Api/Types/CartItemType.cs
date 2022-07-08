using System;
using Core.Entities;
using Api.Resolvers;

namespace Api.Types
{
	public class CartItemType : ObjectType<CartItem>
	{
		public CartItemType()
		{
		}
        protected override void Configure(IObjectTypeDescriptor<CartItem> descriptor)
        {
            descriptor.Field(_ => _.Id);
            descriptor.Field(_ => _.SessionId);
            descriptor.Field(_ => _.PriceId);
            descriptor.Field(_ => _.Quantity);

            descriptor.Field<ProductPriceResolver>(_ => _.GetPriceInCartAsync(default, default));
            descriptor.Field<ShoppingSessionResolver>(_ => _.GetShoppingSessionByCartAsync(default, default));
        }
    }
}

