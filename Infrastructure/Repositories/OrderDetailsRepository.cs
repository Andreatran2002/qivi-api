using System;
using Core.Entities;
using Core.Repositories;
using Infrastructure.Base;
using Infrastructure.Data.Interfaces;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace Infrastructure.Repositories
{
    public class OrderDetailsRepository : BaseRepository<OrderDetails>, IOrderDetailsRepository
    {
        private readonly ILogger<IOrderDetailsRepository> _logger;
        private readonly IMongoCollection<OrderDetails> collection;


        public OrderDetailsRepository(ICatalogContext catalogContext, ILogger<IOrderDetailsRepository> logger) : base(catalogContext)
        {
            _logger = logger;
            collection = catalogContext.GetCollection<OrderDetails>("OrderDetails");

        }

        public async Task<IEnumerable<OrderDetails>> GetByUserId(string userId)
        {
            return await collection.Find(a => a.UserId == userId).ToListAsync();
        }
    }
}

