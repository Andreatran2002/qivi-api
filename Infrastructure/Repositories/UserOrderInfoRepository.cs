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
        private readonly IMongoCollection<UserOrderInfo> _collection;


        public UserOrderInfoRepository(ICatalogContext catalogContext, ILogger<IUserOrderInfoRepository> logger) : base(catalogContext)
        {
            _logger = logger;
            _collection = catalogContext.GetCollection<UserOrderInfo>("UserOrderInfo");

        }

        
        public IEnumerable<UserOrderInfo> GetByUserId(string userId)
        {

            return  _collection.Find(a => a.UserId == userId).ToList();
        }
    }
}

