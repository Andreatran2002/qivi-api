using System;
using Core.Entities;
using Core.Repositories;
using HotChocolate.Subscriptions;
using Microsoft.AspNetCore.Identity;

namespace Api.Mutations
{
    [ExtendObjectType(nameof(Mutation))]
    public class UserOrderInfoMutation
	{
        private readonly ILogger<IUserOrderInfoRepository> _logger;
        public UserOrderInfoMutation( ILogger<IUserOrderInfoRepository> logger)
        {
            _logger = logger;
        }

        public async Task<UserOrderInfo?> CreateUserOrderInfoAsync(string userId, string recipient, string phoneNumber, string address,
            [Service] IUserOrderInfoRepository userOrderInfoRepository, [Service] ITopicEventSender eventSender, [Service] IUserRepository userRepository)
        {
            try
            {
                var accountAvailable = await userRepository.GetByIdAsync(userId);

                if (accountAvailable != null)
                {
                    _logger.LogInformation($"Create new user order info successful");
                    var orderInfo = new UserOrderInfo(userId, recipient, phoneNumber, address);
                    return await userOrderInfoRepository.InsertAsync(orderInfo); ;
                }
                else
                {
                    _logger.LogInformation($"Create user order info failure . User is not available.UserId = {userId}",userId);
                    return null;
                }

            }
            catch (Exception e)
            {
                _logger.LogInformation($"Create user order info failure . Id : {userId}");
             
                return null;
            }


        }


        

        public UserOrderInfo UpdateUserOrderInfo(UserOrderInfo info, [Service] IUserOrderInfoRepository userOrderInfoRepository, [Service] ITopicEventSender eventSender)
        {
            var result = userOrderInfoRepository.Update(info);

            return result;
        }
    }
}

