using System;
using Core.Entities;
using Core.Repositories;
using Infrastructure.Base;
using Infrastructure.Data.Interfaces;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace Infrastructure.Repositories
{
	public class UserOrderInfoRepository : BaseRepository<UserOrderInfo>,  IUserOrderInfoRepository
    {
        private readonly ILogger<IUserOrderInfoRepository> _logger;
        private readonly IMongoCollection<UserOrderInfo> collection;


        public UserOrderInfoRepository(ICatalogContext catalogContext, ILogger<IUserOrderInfoRepository> logger) : base(catalogContext)
        {
            _logger = logger;
            collection = catalogContext.GetCollection<UserOrderInfo>("UserOrderInfo");

        }

        
        public async Task<IEnumerable<UserOrderInfo>> GetByUserId(string userId)
        {
            return await collection.Find(a => a.UserId == userId).ToListAsync();
        }
    }
}

