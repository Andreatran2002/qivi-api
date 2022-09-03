using System;
using Api.Resolvers;
using Core.Entities;

namespace Api.Types
{
    public class UserOrderInfoType : ObjectType<UserOrderInfo>
    {
        protected override void Configure(IObjectTypeDescriptor<UserOrderInfo> descriptor)
        {
            descriptor.Field(_ => _.Id);
            descriptor.Field(_ => _.UserId);
            descriptor.Field(_ => _.PhoneNumber);
            descriptor.Field(_ => _.Address);
            descriptor.Field(_ => _.Recipient);
            
            descriptor.Field<UserResolver>(_ => _.GetUserInfoAsync(default, default));


        }
    
	}
}

