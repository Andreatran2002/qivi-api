using System;
using Core.Entities;
using Core.Repositories;
using Infrastructure.Base;
using Infrastructure.Data.Interfaces;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace Infrastructure.Repositories
{
    public class ShoppingSessionRepository : BaseRepository<ShoppingSession>, IShoppingSessionRepository
    {
        private readonly ILogger<IShoppingSessionRepository> _logger;
        private readonly IMongoCollection<ShoppingSession> collection;
       

        public ShoppingSessionRepository(ICatalogContext catalogContext, ILogger<IShoppingSessionRepository> logger) : base(catalogContext)
        {
            _logger = logger;
            collection = catalogContext.GetCollection<ShoppingSession>("ShoppingSession");

        }

        public async Task<ShoppingSession> GetByUserId(string userId)
        {
            return await collection.Find(a => a.UserId == userId).FirstOrDefaultAsync(); 
        }
    }
}

