using System;
using Core.Entities;
using Core.Repositories;
using HotChocolate.Execution;
using HotChocolate.Subscriptions;

namespace Api.Subscriptions
{
	[ExtendObjectType(Name ="Subscription")]
	public class CustomerSubscription
	{
		[Subscribe]
		public ApplicationUser OnCreateCustomer([EventMessage] ApplicationUser user) => user;
        [SubscribeAndResolve]
        public async ValueTask<ISourceStream
        <List<ApplicationUser>>> OnUsersGet([Service]
        ITopicEventReceiver eventReceiver,
           CancellationToken cancellationToken)
        {
            return await eventReceiver.SubscribeAsync<string,
            List<ApplicationUser>>("ReturnedUsers",
            cancellationToken);
        }
    }
}

