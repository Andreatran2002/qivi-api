using System;
using Core.Base;
using Core.Entities;
using Core.Interfaces;

namespace Core.Repositories
{
	public interface IUserOrderInfoRepository : IBaseRepository<UserOrderInfo>
    {
        public IEnumerable<UserOrderInfo> GetByUserId(string userId);

    }
}

