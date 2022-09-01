using System;
using Core.Base;
using Core.Entities;
using Core.Repositories;

namespace Api.Queries
{
    [ExtendObjectType(nameof(Query))]

    public class UserOrderInfoQuery
	{
		public UserOrderInfoQuery()
		{
		}
        public AppResponse<IEnumerable<UserOrderInfo>> GetRecipientInfoByUserIdAsync(String id , [Service] IUserOrderInfoRepository userOrderInfoRepo, [Service] IUserRepository userRepository)
        {
            try
            {
                return new AppResponse<IEnumerable<UserOrderInfo>> (userOrderInfoRepo.GetByUserId(id).ToList());

            }catch(Exception e)
            {
                return new AppResponse<IEnumerable<UserOrderInfo>>("undefined-error");
            }
        }


    }
}

