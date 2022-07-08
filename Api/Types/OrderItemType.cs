using System;
using Api.Resolvers;
using Core.Entities;

namespace Api.Types
{
	
    public class OrderItemType : ObjectType<OrderItem>
    {
        protected override void Configure(IObjectTypeDescriptor<OrderItem> descriptor)
        {
            descriptor.Field(_ => _.Id);
            descriptor.Field(_ => _.OrderId);
            descriptor.Field(_ => _.PriceId);
            descriptor.Field(_ => _.CreatedAt);
            descriptor.Field(_ => _.ModifiedAt);
            descriptor.Field(_ => _.Quantity);


            descriptor.Field<OrderDetailsResolver>(_ => _.GetOrderDetailsByItems(default, default));
            descriptor.Field<ProductPriceResolver>(_ => _.GetPriceInOrderAsync(default, default));
        }
    }
}

