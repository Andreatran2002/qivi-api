using System;
using Api.Resolvers;
using Core.Entities;

namespace Api.Types
{

    public class UserType : ObjectType<ApplicationUser>
    {
        protected override void Configure(IObjectTypeDescriptor<ApplicationUser> descriptor)
        {
            descriptor.Field(_ => _.Id);
            descriptor.Field(_ => _.UserName);
            descriptor.Field(_ => _.FullName);
            descriptor.Field(_ => _.PhoneNumber);
            descriptor.Field(_ => _.LockoutEnabled);
            descriptor.Field(_ => _.LockoutEnd);
            descriptor.Field(_ => _.AccessFailedCount);
            descriptor.Field(_ => _.Claims);
            descriptor.Field(_ => _.CreatedOn);
            descriptor.Field(_ => _.Email);
            descriptor.Field(_ => _.EmailConfirmed);
            descriptor.Field(_ => _.NormalizedEmail);
            descriptor.Field(_ => _.NormalizedUserName);
            descriptor.Field(_ => _.PhoneNumberConfirmed);
            descriptor.Field(_ => _.ConcurrencyStamp);
            descriptor.Field(_ => _.Tokens);
            descriptor.Field(_ => _.Roles);
            descriptor.Field(_ => _.PasswordHash);
            descriptor.Field(_ => _.Logins);
            descriptor.Field(_ => _.SecurityStamp);
            descriptor.Field(_ => _.TwoFactorEnabled);
            descriptor.Field(_ => _.Version);

            descriptor.Field<UserOrderInfoResolver>(_ => _.GetUserInfo(default, default));


        }
    }
}

