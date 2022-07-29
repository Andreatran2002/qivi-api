using System;
using Api.Resolvers;
using Core.Entities;

namespace Api.Types
{

    public class UserType : ObjectType<User>
    {
        protected override void Configure(IObjectTypeDescriptor<User> descriptor)
        {
            descriptor.Field(_ => _.Id);
            descriptor.Field(_ => _.UserName);
            descriptor.Field(_ => _.Address);
            descriptor.Field(_ => _.FullName);
            descriptor.Field(_ => _.PhoneNumber);

            descriptor.Field<UserOrderInfoResolver>(_ => _.GetUserInfoAsync(default, default));


        }
    }
}

