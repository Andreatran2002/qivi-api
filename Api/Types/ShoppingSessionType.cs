using System;
using Core.Entities;
using Api.Resolvers;

namespace Api.Types
{
    public class ShoppingSessionType : ObjectType<ShoppingSession>
    {
        protected override void Configure(IObjectTypeDescriptor<ShoppingSession> descriptor)
        {
            descriptor.Field(_ => _.Id);
            descriptor.Field(_ => _.UserId);
            descriptor.Field(_ => _.Total);
            descriptor.Field(_ => _.ModifiedAt);
            descriptor.Field(_ => _.CreatedAt);

            descriptor.Field<UserResolver>(_ => _.GetUserByShoppingSessionAsync(default, default));
            descriptor.Field<CartItemResolver>(_ => _.GetCartsBySessionIdAsync(default, default));
        }
    }
}

