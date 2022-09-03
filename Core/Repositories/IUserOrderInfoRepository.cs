using System;
using Core.Base;
using Core.Entities;

namespace Core.Repositories
{
	public interface IUserOrderInfoRepository : IBaseRepository<UserOrderInfo>
    {
        public IEnumerable<UserOrderInfo> GetByUserId(string userId);

    }
} 

