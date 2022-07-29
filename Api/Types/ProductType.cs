using System;
using Core.Entities;
using HotChocolate.Types;
using Api.Resolvers;

namespace Api.Types
{
    public class ProductType : ObjectType<Product>
    {
        protected override void Configure(IObjectTypeDescriptor<Product> descriptor)
        {
            descriptor.Field(_ => _.Id);
            descriptor.Field(_ => _.CategoryId);
            descriptor.Field(_ => _.DiscountId);
            descriptor.Field(_ => _.CreatedAt);
            descriptor.Field(_ => _.ModifiedAt);
            descriptor.Field(_ => _.Name);
            descriptor.Field(_ => _.Description);
            descriptor.Field(_ => _.IsEmpty);
            descriptor.Field(_ => _.Image);


            descriptor.Field<CategoryResolver>(_ => _.GetCategoryAsync(default, default));
            descriptor.Field<DiscountResolver>(_ => _.GetDiscountByProductAsync(default, default));
            descriptor.Field<ProductPriceResolver>(_ => _.GetPricesByProductIdAsync(default, default));
        }
    }
} 

