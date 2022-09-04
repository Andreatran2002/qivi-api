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
            descriptor.Field(_ => _.FirstName);
            descriptor.Field(_ => _.LastName);
            descriptor.Field(_ => _.PhoneNumber);

            descriptor.Field<UserOrderInfoResolver>(_ => _.GetUserInfoAsync(default, default));


        }
    }
}

