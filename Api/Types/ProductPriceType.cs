using System;
using Api.Resolvers;
using Core.Entities;

namespace Api.Types
{
	public class ProductPriceType : ObjectType<ProductPrice>
	{
		
        protected override void Configure(IObjectTypeDescriptor<ProductPrice> descriptor)
        {
            descriptor.Field(_ => _.Id);
            descriptor.Field(_ => _.SKU);
            descriptor.Field(_ => _.ProductId);
            descriptor.Field(_ => _.Price);


            descriptor.Field<ProductResolver>(_ => _.GetProductAsync(default, default));
        }
    }
}

