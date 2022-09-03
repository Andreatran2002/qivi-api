using System;
using Core.Entities;
using Core.Repositories;
using HotChocolate.Subscriptions;
using MongoDB.Driver;

namespace Api.Queries
{
	[ExtendObjectType(nameof(Query))]

	public class UserQuery
	{
		[UseFiltering]
		public async Task<IEnumerable<ApplicationUser>> GetUsers([Service] IUserRepository userRepository, [Service] ITopicEventSender eventSender)
        {
			var users =  await userRepository.GetAllAsync();
			return users;  
		}

		
        public async Task<ApplicationUser> GetUserByIdAsync(string id, [Service] IUserRepository userRepository, [Service] ITopicEventSender eventSender)
       => await userRepository.GetByIdAsync(id);

        public async Task<ApplicationUser> GetUserByPhoneAsync(string phoneNumber, [Service] IUserRepository userRepository, [Service] ITopicEventSender eventSender)
        => await userRepository.GetUserByPhoneNumber(phoneNumber);

    }
}
